# CoinMarketCapDotNet v2 Modernization — Design

**Date:** 2026-04-21
**Status:** Approved
**Target release:** v2.0.0 on NuGet

## Goal

Modernize the `CoinMarketCapDotNet` API wrapper so it requires fewer dependencies, runs on modern .NET, and exposes a clean, idiomatic API surface — without sacrificing reach for legacy .NET Framework consumers.

## Current state (problems being solved)

- `CoinMarketCapDotNet.csproj` and `CoinMarketCapDotNet_Tests.csproj` are legacy MSBuild XML targeting `net472` only.
- Both projects use `packages.config` with hard-coded `..\packages\...` HintPath references (notably `Newtonsoft.Json 13.0.3`).
- The library carries a permanent runtime dependency on `Newtonsoft.Json` (~576 `[JsonProperty]` attributes across 80 model files).
- `CoinMarketCapAPI.GetDataAsync<T>` throws raw `System.Exception` for every failure mode and accepts no `CancellationToken`.
- The constructor is hard-wired (`apiKey`, `useSandbox` bool); there is no DI/options support and no way to inject a custom `HttpClient`.
- Tests project mixes both MSTest and xUnit packages even though only one is in use.
- `AssemblyInfo.cs` carries manual `1.0.0.0` versioning while NuGet ships `1.0.10` — drift between source and packages.
- One stray `using CoinMarketCapSdk.Models.Cryptocurrency.Categories;` at `CoinMarketCapAPI.cs:63` indicates a model class sits under the wrong root namespace.

## Decisions

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Target frameworks | `netstandard2.0;net8.0` | Maximum reach (legacy .NET Framework 4.6.1+ via netstandard2.0) plus zero-dep modern path on .NET 8 |
| JSON serializer | `System.Text.Json` | Built-in on .NET 8 (zero runtime deps); ~2× faster; lower allocations |
| API versioning | v2.0.0 — breaking changes allowed | Free to fix the API shape; consumers stay on 1.x until they migrate |
| csproj style | SDK-style for both projects | Unblocks multi-targeting, modern packing, and SDK-generated assembly metadata |
| Test framework | xUnit only (drop MSTest) | Existing tests already use xUnit; MSTest packages are dead weight |
| Tests TFM | `net8.0` only | Tests do not ship and do not need legacy-framework reach |

## Architecture

The wrapper structure is unchanged at the consumer level: `CoinMarketCapAPI` exposes nine endpoint groups (`Cryptocurrency`, `Fiat`, `Exchange`, `GlobalMetrics`, `Tools`, `Blockchain`, `Key`, `Content`, `Community`), each a nested class with one method per HTTP endpoint. The HTTP transport, JSON deserialization, and exception mapping all live in `CoinMarketCapAPI.GetDataAsync<T>`. Endpoint URL fragments stay in `Api/General/Endpoints.cs`. Models stay one-class-per-file under `Models/<Category>/...`.

What changes is the foundation under that structure: the build system, the JSON library, and the public method signatures.

## Section 1 — Build & framework modernization

### 1.1 Library project (`CoinMarketCapDotNet.csproj`)

- Convert to SDK-style: `<Project Sdk="Microsoft.NET.Sdk">`.
- Replace `<TargetFrameworkVersion>` with `<TargetFrameworks>netstandard2.0;net8.0</TargetFrameworks>`.
- Remove the per-file `<Compile Include="...">` block — SDK-style globs `**/*.cs` automatically.
- Remove `<Reference Include="System..." />` blocks — implicit on SDK-style.
- Enable: `<Nullable>enable</Nullable>`, `<LangVersion>latest</LangVersion>`, `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`, `<GenerateDocumentationFile>true</GenerateDocumentationFile>`, `<Deterministic>true</Deterministic>`.
- NuGet packaging metadata in csproj:
  - `<PackageId>CoinMarketCapDotNet</PackageId>`
  - `<Version>2.0.0</Version>`
  - `<Authors>Murat Sanlisavas</Authors>`
  - `<Description>A modern .NET wrapper for the CoinMarketCap API. Multi-targets netstandard2.0 and net8.0 with zero runtime dependencies on .NET 8.</Description>
  - `<RepositoryUrl>` and `<RepositoryType>git</RepositoryType>`
  - `<PackageProjectUrl>`
  - `<PackageLicenseExpression>MIT</PackageLicenseExpression>` (replaces the file-based `LICENSE.txt` content reference)
  - `<PackageReadmeFile>README.md</PackageReadmeFile>` with `<None Include="..\README.md" Pack="true" PackagePath="\" />`
  - `<PackageTags>coinmarketcap;cmc;crypto;cryptocurrency;api;wrapper;sdk</PackageTags>`
- Conditional dependency on `netstandard2.0` only:
  ```xml
  <ItemGroup Condition="'$(TargetFramework)' == 'netstandard2.0'">
    <PackageReference Include="System.Text.Json" Version="8.0.5" />
  </ItemGroup>
  ```
  On `net8.0` `System.Text.Json` is part of the shared framework — no PackageReference needed.

### 1.2 Tests project (`CoinMarketCapDotNet_Tests.csproj`)

- Convert to SDK-style.
- `<TargetFramework>net8.0</TargetFramework>`.
- Drop all MSTest packages and references.
- `PackageReference` to current xUnit: `xunit` 2.9.x, `xunit.runner.visualstudio` 2.8.x, `Microsoft.NET.Test.Sdk` 17.x.
- Project reference back to the library is preserved.

### 1.3 Repo-level

- Add `Directory.Build.props` at the repo root with shared metadata (Authors, Company, Copyright, RepositoryUrl) and source-link config (`<PublishRepositoryUrl>true</PublishRepositoryUrl>`, `<EmbedUntrackedSources>true</EmbedUntrackedSources>`, `Microsoft.SourceLink.GitHub` PackageReference).
- Delete legacy `packages/` lockfile artifacts and `packages.config` files.
- Delete `Properties/AssemblyInfo.cs` from both projects (SDK generates equivalent attributes).

## Section 2 — JSON migration (Newtonsoft → System.Text.Json)

### 2.1 Mechanical rename

Across all model files under `CoinMarketCapDotNet/Models/`:

- `using Newtonsoft.Json;` → `using System.Text.Json.Serialization;`
- `[JsonProperty("foo")]` → `[JsonPropertyName("foo")]`
- `[JsonIgnore]` exists in both libraries — no change needed.
- `JsonConvert.DeserializeObject<T>(json)` calls in `CoinMarketCapAPI` → `JsonSerializer.Deserialize<T>(json, options)`.

### 2.2 Shared `JsonSerializerOptions`

A single static (or instance-cached) `JsonSerializerOptions` is configured once in `CoinMarketCapAPI`:

```csharp
private static readonly JsonSerializerOptions JsonOptions = new()
{
    PropertyNameCaseInsensitive = true,
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Converters = { new JsonStringEnumConverter() }
};
```

Rationale for each option:
- `PropertyNameCaseInsensitive = true` mirrors Newtonsoft's lenient default; CMC payloads are camelCase-ish but historically Newtonsoft tolerated mismatches.
- `NumberHandling = AllowReadingFromString` covers cases where CMC returns numerics quoted (sandbox vs live divergence is documented in README).
- `JsonStringEnumConverter` — CMC returns enum-like fields as strings.
- `DefaultIgnoreCondition = WhenWritingNull` is forward-looking (the wrapper currently only deserializes, but if request bodies are ever serialized this avoids null spam).

### 2.3 Custom converters

If any model relies on a Newtonsoft-specific behavior not covered by the options above (e.g. flexible date parsing, dictionaries with non-string keys), add a targeted `JsonConverter<T>` rather than reverting the whole library. These will surface during the test pass.

### 2.4 Validation

- Run the full test suite against the CMC sandbox.
- Spot-check the endpoints flagged in the README changelog as having historical serialization issues: `OHLCVHistorical`, `OHLCVLatest`, `GetTrendingMostVisited`, `GetTrendingLatest`, `GetGainersLosers`, `GetInfoDataAsync`, `PricePerformanceStats`, latest quotes.

## Section 3 — Public API surface (v2.0.0 breaking changes)

### 3.1 Constructor — options pattern

```csharp
public sealed class CoinMarketCapOptions
{
    public required string ApiKey { get; init; }
    public bool UseSandbox { get; init; }
    public Uri? BaseAddress { get; init; }      // overrides UseSandbox
    public TimeSpan? Timeout { get; init; }     // overrides HttpClient default
}

public class CoinMarketCapAPI
{
    public CoinMarketCapAPI(CoinMarketCapOptions options, HttpClient? httpClient = null);

    // Convenience overload — preserved for the simple case
    public CoinMarketCapAPI(string apiKey, bool useSandbox = false);
}
```

- When `httpClient` is `null`, the wrapper uses a process-wide static `HttpClient` (the current behavior).
- When `httpClient` is supplied, it is used as-is — enables `IHttpClientFactory`, custom `DelegatingHandler` chains for retry/logging, and test mocking.
- `required` and `init` keep `CoinMarketCapOptions` immutable and force `ApiKey` to be set at construction.

### 3.2 `CancellationToken` on every async method

Every `Task<...>` method on every endpoint group gains a trailing `CancellationToken cancellationToken = default` parameter. Tokens are forwarded to `HttpClient.SendAsync` and `HttpContent.ReadAsStringAsync` (where supported on the TFM).

### 3.3 Typed exception hierarchy

Replace every `throw new Exception(...)` in `GetDataAsync<T>` with a typed exception:

```csharp
public class CoinMarketCapException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public int? ErrorCode { get; }            // CMC's status.error_code
    public string? CmcErrorMessage { get; }   // CMC's status.error_message
    // ...constructors
}

public sealed class CoinMarketCapBadRequestException : CoinMarketCapException { }   // 400
public sealed class CoinMarketCapAuthException        : CoinMarketCapException { }   // 401, 403
public sealed class CoinMarketCapRateLimitException   : CoinMarketCapException { }   // 429
public sealed class CoinMarketCapServerException      : CoinMarketCapException { }   // 5xx
```

The mapping table inside `GetDataAsync<T>` becomes a `switch` from `HttpStatusCode` to the right exception type, attaching the parsed `Status` block. Network-level failures (`HttpRequestException`, `TaskCanceledException`) bubble up unwrapped.

### 3.4 Nullable reference types

Annotate the public API and all model properties. CMC payloads commonly omit fields, so most response model properties become nullable. The annotation pass also doubles as a quick sanity check that the model definitions match observed payloads.

### 3.5 Bug fixes folded in

- The `using CoinMarketCapSdk.Models.Cryptocurrency.Categories;` at `CoinMarketCapAPI.cs:63` indicates `CategoriesData` lives under root namespace `CoinMarketCapSdk` instead of `CoinMarketCapDotNet`. Move it under the correct namespace and remove the stray `using`.

### 3.6 Out of scope (YAGNI)

The following improvements are deliberately deferred — they have real value but are not required for the v2 modernization goal:

- Built-in retry / `Polly` integration. Consumers can layer their own `DelegatingHandler` via the injected `HttpClient`.
- `IAsyncEnumerable<T>` for paginated endpoints.
- `System.Text.Json` source generators (defer to v2.1; pure runtime reflection works for the migration).
- A `services.AddCoinMarketCap(...)` `IServiceCollection` extension. The injectable `HttpClient` already enables DI; an explicit extension can ship in a separate `CoinMarketCapDotNet.DependencyInjection` package later if there's demand.

## Section 4 — Migration guide

Two artifacts:

### 4.1 New file: `MIGRATION-V1-TO-V2.md` at repo root

Sections, in order:

1. **What changed and why** — short summary of the modernization goals.
2. **Framework requirements** — table mapping consumer TFM to support status (NF 4.6.1+ supported via netstandard2.0; .NET Core/5+ supported via net8.0; NF < 4.6.1 no longer supported).
3. **Dependency changes** — Newtonsoft.Json removed; on .NET 8 the library has zero runtime deps; on netstandard2.0 the only dep is `System.Text.Json`.
4. **Constructor migration** — side-by-side v1 vs v2 snippets (simple case and options-pattern case, plus `IHttpClientFactory` example).
5. **Exception handling migration** — code samples showing `catch (Exception)` → `catch (CoinMarketCapRateLimitException)` etc.
6. **CancellationToken** — example of plumbing one through a typical call.
7. **JSON payload shape** — explicit statement that response models are unchanged at the property-name level; only the attribute namespace changed. Most consumer code that just reads `Response<T>.Data.Whatever` works as-is.
8. **DI sample** — a compact ASP.NET Core registration example using `IHttpClientFactory`.
9. **Common pitfalls** — case sensitivity (handled by options), enum value casing, etc.

### 4.2 README polish

- Move the "Known Issues" block down into its own section near the bottom (currently it's the very first thing in the file).
- Add a v2 install snippet at the top of "Installation".
- Add a prominent "Migrating from v1?" link to `MIGRATION-V1-TO-V2.md` at the top of the README, just above the install snippet.
- Update the "Usage" code samples to show the new constructor and the typed exception catch.
- Remove the manual changelog from the README and link to GitHub Releases instead (or keep a short "Release Notes" section that points to the full changelog).

## Section 5 — Testing & cleanup

- Migrate the tests csproj to SDK-style targeting `net8.0`.
- Drop MSTest packages and adapter; keep only xUnit + `Microsoft.NET.Test.Sdk` + `xunit.runner.visualstudio`.
- Verify the existing fixture (`CoinMarketCapAPICollection`, `CoinMarketCapAPIFixture`) still wires up correctly under the new packages.
- Run the suite end-to-end against the CMC sandbox; document any deserialization regressions and fix with targeted converters.
- Delete the legacy `packages/` directory if present after the migration completes.
- Verify the assembly's `XML` doc file is generated and packed (it was being produced under v1).

## Risks and mitigations

| Risk | Mitigation |
|------|------------|
| STJ deserialization differs from Newtonsoft on a real CMC payload | Spot-check the endpoints flagged in the v1 changelog as historically problematic; add per-type converters where necessary; document any remaining gaps |
| `required`/`init` features unavailable on netstandard2.0 by default | Polyfill `IsExternalInit` (one-line internal class); `required` works with `LangVersion=latest` plus the `RequiredMemberAttribute` polyfill — or fall back to a constructor parameter on options if polyfilling proves noisy |
| Consumers blindly `catch (Exception)` will keep working but will catch the new typed exceptions transparently | Document the migration; the new exceptions still derive from `Exception` so old code continues to function |
| Multi-targeting introduces TFM-specific bugs | Run tests on both target frameworks in CI (or at least locally before each release) |

## Open questions

None. All questions raised during brainstorming were resolved.

## Acceptance criteria

- `dotnet build` succeeds for both `netstandard2.0` and `net8.0`.
- `dotnet test` passes against the CMC sandbox.
- `dotnet pack` produces a NuGet package with `Version=2.0.0`, embedded README, source link, and an XML doc file.
- The package shows zero runtime dependencies on `net8.0` and a single `System.Text.Json` dependency on `netstandard2.0`.
- `MIGRATION-V1-TO-V2.md` exists at repo root and is linked from `README.md`.
- No file in `Models/` references `Newtonsoft.Json`.
- No `packages.config`, no `AssemblyInfo.cs`, no legacy `<Reference Include="System...">` blocks remain.

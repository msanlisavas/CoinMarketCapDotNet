# CoinMarketCapDotNet v2 Modernization — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Modernize the CoinMarketCapDotNet NuGet wrapper to multi-target `netstandard2.0;net8.0`, swap Newtonsoft.Json for System.Text.Json, modernize the public API surface (typed exceptions, CancellationToken, options pattern), and ship as v2.0.0 with a v1→v2 migration guide.

**Architecture:** Same nine endpoint groups (`Cryptocurrency`, `Fiat`, `Exchange`, `GlobalMetrics`, `Tools`, `Blockchain`, `Key`, `Content`, `Community`) hanging off a `CoinMarketCapAPI` root class with a single `GetDataAsync<T>` transport. Both projects converted to SDK-style csproj. Library multi-targets; test project is `net8.0` only. JSON deserialization uses a single shared `JsonSerializerOptions`. `HttpClient` is injectable to enable DI and test stubbing.

**Tech Stack:** .NET 8 SDK, `netstandard2.0`/`net8.0`, `System.Text.Json` 8.x (built-in on net8, NuGet on netstandard2.0), `xUnit` 2.9.x, `Microsoft.NET.Test.Sdk` 17.x, `Microsoft.SourceLink.GitHub`.

**Commit policy:** Every task ends with a commit. Commit messages do **not** include any AI co-author trailer — the repo owner is the sole author of all commits.

---

## File Structure

**Files created:**
- `Directory.Build.props` — repo-root shared build settings.
- `CoinMarketCapDotNet/Polyfills/IsExternalInit.cs` — netstandard2.0-only polyfill so `init` setters compile.
- `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapException.cs` — base typed exception.
- `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapBadRequestException.cs`
- `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapAuthException.cs`
- `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapRateLimitException.cs`
- `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapServerException.cs`
- `CoinMarketCapDotNet/Configuration/CoinMarketCapOptions.cs` — options-pattern config object.
- `CoinMarketCapDotNet_Tests/StubHandlers/StubHttpMessageHandler.cs` — test infrastructure.
- `CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs` — new unit tests (no live API key).
- `MIGRATION-V1-TO-V2.md` — v1→v2 migration guide.

**Files rewritten:**
- `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj` — SDK-style.
- `CoinMarketCapDotNet_Tests/CoinMarketCapDotNet_Tests.csproj` — SDK-style.
- `README.md` — restructured with v2 install, link to migration guide, polished code samples.

**Files modified in place:**
- `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs` — new constructor, `JsonSerializerOptions`, typed exception throws, `CancellationToken` on every method, fixed stray `using`.
- All 80 model files under `CoinMarketCapDotNet/Models/**/*.cs` — `Newtonsoft.Json` → `System.Text.Json.Serialization`, `[JsonProperty]` → `[JsonPropertyName]`.
- `CoinMarketCapDotNet/Models/Cryptocurrency/Categories/CategoriesData.cs` — namespace fix `CoinMarketCapSdk.Models.Cryptocurrency.Categories` → `CoinMarketCapDotNet.Models.Cryptocurrency.Categories`.
- `CoinMarketCapDotNet_Tests/Fixture/CoinMarketCapAPIFixture.cs` — adopt new constructor.
- `CoinMarketCapDotNet_Tests/CoinMarketCapAPITests.cs` — adopt new constructor + add `CancellationToken` defaults to existing call sites where it improves them (no behavior change).

**Files deleted:**
- `CoinMarketCapDotNet/Properties/AssemblyInfo.cs`
- `CoinMarketCapDotNet/packages.config`
- `CoinMarketCapDotNet_Tests/Properties/AssemblyInfo.cs`
- `CoinMarketCapDotNet_Tests/packages.config`
- `packages/` (top-level NuGet packages folder, if present)

---

## Important conventions for the implementer

1. **Working directory** is the repo root: `C:\Users\Murat\source\repos\msanlisavas\CoinMarketCapDotNet` (Windows, bash shell — use forward slashes in file paths inside commands).
2. Run `dotnet` commands from the repo root unless otherwise noted.
3. **Existing integration tests require a real CMC sandbox API key** hard-coded in `CoinMarketCapAPIFixture.cs` (placeholder `"your-valid-api-key"`). Do **not** commit a real key. After each major task, run only the new unit tests (which use a stub HTTP handler) plus `dotnet build` to confirm the integration-test code still compiles.
4. **Commit messages** must be plain — no `Co-Authored-By` trailer, no AI attribution. Use `git commit -m "..."` only.
5. After every task, verify the build succeeds for both target frameworks: `dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj`. The TFM list is in the csproj.

---

## Task 1: Establish baseline

**Files:** none modified.

- [ ] **Step 1.1: Confirm `dotnet --version` returns ≥ 8.0**

```bash
dotnet --version
```

Expected: `8.0.x` or newer. If it fails, install the .NET 8 SDK before continuing.

- [ ] **Step 1.2: Restore the legacy NuGet packages**

```bash
dotnet restore CoinMarketCapDotNet.sln
```

Expected: succeeds, restoring `Newtonsoft.Json 13.0.3`, xUnit 2.4.2, MSTest 2.2.10, etc., into `packages/`. If `dotnet restore` cannot handle `packages.config`, run `nuget restore CoinMarketCapDotNet.sln` instead.

- [ ] **Step 1.3: Build the solution as-is**

```bash
dotnet build CoinMarketCapDotNet.sln -c Release
```

Expected: build succeeds. Record any warnings — they are baseline noise to compare against later.

- [ ] **Step 1.4: Do not run integration tests**

The existing tests require a live API key. We're capturing build-state baseline only. No commit in this task.

---

## Task 2: Convert library project to SDK-style multi-targeting

**Files:**
- Rewrite: `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj`
- Delete: `CoinMarketCapDotNet/packages.config`
- Delete: `CoinMarketCapDotNet/Properties/AssemblyInfo.cs`

This task keeps `Newtonsoft.Json` as a `PackageReference` so the existing source still compiles. JSON migration happens in Task 5.

- [ ] **Step 2.1: Replace the csproj with the SDK-style version**

Overwrite `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj` with exactly:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFrameworks>netstandard2.0;net8.0</TargetFrameworks>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <RootNamespace>CoinMarketCapDotNet</RootNamespace>
    <AssemblyName>CoinMarketCapDotNet</AssemblyName>
    <Deterministic>true</Deterministic>

    <PackageId>CoinMarketCapDotNet</PackageId>
    <Version>2.0.0</Version>
    <Authors>Murat Sanlisavas</Authors>
    <Description>A modern .NET wrapper for the CoinMarketCap API. Multi-targets netstandard2.0 and net8.0 with zero runtime dependencies on .NET 8.</Description>
    <PackageTags>coinmarketcap;cmc;crypto;cryptocurrency;api;wrapper;sdk</PackageTags>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <PackageReadmeFile>README.md</PackageReadmeFile>
    <PackageProjectUrl>https://github.com/msanlisavas/CoinMarketCapDotNet</PackageProjectUrl>
    <RepositoryUrl>https://github.com/msanlisavas/CoinMarketCapDotNet</RepositoryUrl>
    <RepositoryType>git</RepositoryType>
  </PropertyGroup>

  <ItemGroup>
    <None Include="..\README.md" Pack="true" PackagePath="\" />
  </ItemGroup>

  <!-- Newtonsoft.Json kept temporarily; removed in Task 6 once STJ migration completes. -->
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
  </ItemGroup>

</Project>
```

- [ ] **Step 2.2: Delete the legacy files**

```bash
rm CoinMarketCapDotNet/packages.config
rm CoinMarketCapDotNet/Properties/AssemblyInfo.cs
rmdir CoinMarketCapDotNet/Properties 2>/dev/null || true
```

- [ ] **Step 2.3: Build the library on both target frameworks**

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release
```

Expected: build succeeds for both `netstandard2.0` and `net8.0`. Existing nullable-related warnings are expected (resolved in Task 12); they should NOT be errors yet because `<TreatWarningsAsErrors>` is intentionally not enabled until the nullable pass is complete.

If you get errors about types being unresolved (`HttpClient`, `Task`, etc.), the implicit references are missing — verify the csproj uses `Microsoft.NET.Sdk` (not `Microsoft.NET.Sdk.Web` or another variant).

- [ ] **Step 2.4: Commit**

```bash
git add CoinMarketCapDotNet/CoinMarketCapDotNet.csproj
git add -u CoinMarketCapDotNet/packages.config CoinMarketCapDotNet/Properties/AssemblyInfo.cs
git commit -m "Convert library project to SDK-style multi-targeting netstandard2.0 and net8.0"
```

---

## Task 3: Convert tests project to SDK-style targeting net8.0

**Files:**
- Rewrite: `CoinMarketCapDotNet_Tests/CoinMarketCapDotNet_Tests.csproj`
- Delete: `CoinMarketCapDotNet_Tests/packages.config`
- Delete: `CoinMarketCapDotNet_Tests/Properties/AssemblyInfo.cs`

- [ ] **Step 3.1: Replace the tests csproj**

Overwrite `CoinMarketCapDotNet_Tests/CoinMarketCapDotNet_Tests.csproj` with exactly:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <RootNamespace>CoinMarketCapDotNet_Tests</RootNamespace>
    <AssemblyName>CoinMarketCapDotNet_Tests</AssemblyName>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
    <PackageReference Include="xunit" Version="2.9.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\CoinMarketCapDotNet\CoinMarketCapDotNet.csproj" />
  </ItemGroup>

</Project>
```

- [ ] **Step 3.2: Delete the legacy test files**

```bash
rm CoinMarketCapDotNet_Tests/packages.config
rm CoinMarketCapDotNet_Tests/Properties/AssemblyInfo.cs
rmdir CoinMarketCapDotNet_Tests/Properties 2>/dev/null || true
```

- [ ] **Step 3.3: Build the solution end-to-end**

```bash
dotnet build CoinMarketCapDotNet.sln -c Release
```

Expected: succeeds. The MSTest references are gone but no test code uses them, so nothing breaks. Tests still compile against xUnit.

- [ ] **Step 3.4: Commit**

```bash
git add CoinMarketCapDotNet_Tests/CoinMarketCapDotNet_Tests.csproj
git add -u CoinMarketCapDotNet_Tests/packages.config CoinMarketCapDotNet_Tests/Properties/AssemblyInfo.cs
git commit -m "Convert tests project to SDK-style on net8.0; drop MSTest, keep xUnit only"
```

---

## Task 4: Add `Directory.Build.props` and Source Link

**Files:**
- Create: `Directory.Build.props`
- Modify: `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj`

- [ ] **Step 4.1: Create `Directory.Build.props` at the repo root**

```xml
<Project>

  <PropertyGroup>
    <Company>Murat Sanlisavas</Company>
    <Copyright>Copyright (c) Murat Sanlisavas</Copyright>
    <Deterministic>true</Deterministic>
    <ContinuousIntegrationBuild Condition="'$(GITHUB_ACTIONS)' == 'true'">true</ContinuousIntegrationBuild>
    <PublishRepositoryUrl>true</PublishRepositoryUrl>
    <EmbedUntrackedSources>true</EmbedUntrackedSources>
    <IncludeSymbols>true</IncludeSymbols>
    <SymbolPackageFormat>snupkg</SymbolPackageFormat>
  </PropertyGroup>

</Project>
```

- [ ] **Step 4.2: Add the SourceLink package to the library csproj**

Append a new `<ItemGroup>` to `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj` (just before the closing `</Project>` tag):

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.SourceLink.GitHub" Version="8.0.0" PrivateAssets="All" />
  </ItemGroup>
```

- [ ] **Step 4.3: Verify build still succeeds and `dotnet pack` produces a package**

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release
dotnet pack CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release -o ./artifacts
```

Expected: build succeeds; `./artifacts/CoinMarketCapDotNet.2.0.0.nupkg` and `./artifacts/CoinMarketCapDotNet.2.0.0.snupkg` are produced. Confirm with:

```bash
ls artifacts/
```

Expected output contains both files.

- [ ] **Step 4.4: Commit**

```bash
git add Directory.Build.props CoinMarketCapDotNet/CoinMarketCapDotNet.csproj
git commit -m "Add Directory.Build.props with deterministic builds and Source Link"
```

---

## Task 5: Migrate JSON attributes from Newtonsoft to System.Text.Json

**Files:**
- Modify: every file under `CoinMarketCapDotNet/Models/**/*.cs` (80 files).
- Modify: `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs` (using directives + `JsonConvert.DeserializeObject` calls + new `JsonSerializerOptions`).
- Modify: `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj` (add conditional STJ package).

Newtonsoft.Json stays a PackageReference for now; it is removed in Task 6.

- [ ] **Step 5.1: Add the conditional `System.Text.Json` reference to the library csproj**

Insert this `<ItemGroup>` into `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj` (place it next to the existing `Newtonsoft.Json` ItemGroup):

```xml
  <ItemGroup Condition="'$(TargetFramework)' == 'netstandard2.0'">
    <PackageReference Include="System.Text.Json" Version="8.0.5" />
  </ItemGroup>
```

(On `net8.0`, `System.Text.Json` is in the shared framework — no PackageReference needed.)

- [ ] **Step 5.2: Bulk-rename JSON attributes across the Models tree**

Run these from the repo root (Windows bash). They use simple regex via `sed`; verify the count of changes after.

```bash
grep -rl "Newtonsoft.Json" CoinMarketCapDotNet/Models | wc -l
```

Expected count: 80.

```bash
find CoinMarketCapDotNet/Models -type f -name "*.cs" -exec sed -i 's|using Newtonsoft\.Json;|using System.Text.Json.Serialization;|g' {} +
find CoinMarketCapDotNet/Models -type f -name "*.cs" -exec sed -i 's|\[JsonProperty(|[JsonPropertyName(|g' {} +
```

Verify all `[JsonProperty]` references are gone:

```bash
grep -rn "JsonProperty" CoinMarketCapDotNet/Models
```

Expected: no output (the substring `JsonProperty` should not appear anywhere — neither as `[JsonProperty(` nor as `JsonPropertyAttribute`).

Verify the new attribute is present:

```bash
grep -rn "JsonPropertyName" CoinMarketCapDotNet/Models | wc -l
```

Expected: 575+ matches (matches the original count of `[JsonProperty(` occurrences).

If any model file imports a Newtonsoft type other than `JsonProperty` (`JsonConverter`, `JsonExtensionData`, `JsonIgnore`), grep will catch it:

```bash
grep -rn "Newtonsoft" CoinMarketCapDotNet/Models
```

Expected: no output. If output appears, hand-fix those files individually:
- `[JsonIgnore]` → `[JsonIgnore]` (same name, just under `System.Text.Json.Serialization`).
- `[JsonExtensionData]` → `[JsonExtensionData]` (same name, but the property type must be `Dictionary<string, JsonElement>` instead of `Dictionary<string, JToken>`).
- Custom `JsonConverter<T>` subclasses must be rewritten against `System.Text.Json` APIs (read tokens, write values). If you find one, write the conversion inside this task.

- [ ] **Step 5.3: Update `CoinMarketCapAPI.cs` to use `System.Text.Json`**

Open `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs`. Apply these specific edits:

(a) Replace the `using Newtonsoft.Json;` line with:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;
```

(b) Inside the `CoinMarketCapAPI` class, just below the `private static readonly HttpClient client = new HttpClient();` line, add:

```csharp
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() }
        };
```

(c) Replace every `JsonConvert.DeserializeObject<T>(content)` call inside `GetDataAsync<T>` with `JsonSerializer.Deserialize<T>(content, JsonOptions)`. There are five such call sites in the original method (one per HTTP status case + the OK case). Concretely, the `case HttpStatusCode.OK` block becomes:

```csharp
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadAsStringAsync();
                    T? result = JsonSerializer.Deserialize<T>(content, JsonOptions);
                    return result ?? throw new Exception("Failed to deserialize response content.");
```

And each error case becomes (example for BadRequest — apply the same shape to Unauthorized, Forbidden, InternalServerError):

```csharp
                case HttpStatusCode.BadRequest:
                    var badRequestContent = await response.Content.ReadAsStringAsync();
                    var badRequestStatus = JsonSerializer.Deserialize<ResponseDict<Status>>(badRequestContent, JsonOptions);
                    throw new Exception($"Bad request: {badRequestStatus?.Status?.ErrorMessage}");
```

(The raw `Exception` throws are temporary and replaced with typed exceptions in Task 9.)

- [ ] **Step 5.4: Build and confirm both target frameworks compile**

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release
```

Expected: succeeds for both `netstandard2.0` and `net8.0`. There may be nullable warnings; they are addressed in Task 12.

- [ ] **Step 5.5: Commit**

```bash
git add -A CoinMarketCapDotNet/Models CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs CoinMarketCapDotNet/CoinMarketCapDotNet.csproj
git commit -m "Migrate JSON serialization from Newtonsoft.Json to System.Text.Json"
```

---

## Task 6: Remove Newtonsoft.Json dependency

**Files:** Modify `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj`.

- [ ] **Step 6.1: Verify nothing in the source still references `Newtonsoft`**

```bash
grep -rn "Newtonsoft" CoinMarketCapDotNet/
```

Expected: no output. If any line appears (unlikely, given Step 5.2's verification), fix it before continuing.

- [ ] **Step 6.2: Remove the Newtonsoft PackageReference**

Edit `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj` and delete this entire `ItemGroup`:

```xml
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
  </ItemGroup>
```

- [ ] **Step 6.3: Confirm clean rebuild**

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release --no-incremental
```

Expected: succeeds for both TFMs with no `Newtonsoft.Json` resolution.

- [ ] **Step 6.4: Confirm the published package has zero runtime deps on net8.0**

```bash
dotnet pack CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release -o ./artifacts
```

Then list the package's dependency groups:

```bash
unzip -p artifacts/CoinMarketCapDotNet.2.0.0.nupkg CoinMarketCapDotNet.nuspec | grep -A5 "<dependencies"
```

Expected: under `<group targetFramework=".NETStandard2.0">` only `System.Text.Json` appears; under `<group targetFramework="net8.0">` either no dependencies or only framework-implicit ones.

- [ ] **Step 6.5: Commit**

```bash
git add CoinMarketCapDotNet/CoinMarketCapDotNet.csproj
git commit -m "Remove Newtonsoft.Json dependency now that STJ migration is complete"
```

---

## Task 7: Fix `CategoriesData` namespace bug

**Files:**
- Modify: `CoinMarketCapDotNet/Models/Cryptocurrency/Categories/CategoriesData.cs`
- Modify: `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs`

- [ ] **Step 7.1: Move `CategoriesData` under the correct root namespace**

In `CoinMarketCapDotNet/Models/Cryptocurrency/Categories/CategoriesData.cs`, change the namespace declaration from:

```csharp
namespace CoinMarketCapSdk.Models.Cryptocurrency.Categories
```

to:

```csharp
namespace CoinMarketCapDotNet.Models.Cryptocurrency.Categories
```

- [ ] **Step 7.2: Remove the stray `using` from the API class**

In `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs`, delete the line:

```csharp
using CoinMarketCapSdk.Models.Cryptocurrency.Categories;
```

Add this in its place (alphabetical order):

```csharp
using CoinMarketCapDotNet.Models.Cryptocurrency.Categories;
```

- [ ] **Step 7.3: Build to confirm no other file referenced the old namespace**

```bash
dotnet build CoinMarketCapDotNet.sln -c Release
```

Expected: succeeds. If it fails with "type or namespace `CoinMarketCapSdk` could not be found", grep:

```bash
grep -rn "CoinMarketCapSdk" .
```

and update each occurrence to `CoinMarketCapDotNet`.

- [ ] **Step 7.4: Commit**

```bash
git add CoinMarketCapDotNet/Models/Cryptocurrency/Categories/CategoriesData.cs CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs
git commit -m "Fix CategoriesData namespace from CoinMarketCapSdk to CoinMarketCapDotNet"
```

---

## Task 8: Add `IsExternalInit` polyfill (netstandard2.0 only)

**Files:** Create `CoinMarketCapDotNet/Polyfills/IsExternalInit.cs`.

C# 9+ `init` setters require the compiler to see `System.Runtime.CompilerServices.IsExternalInit`. .NET 5+ defines it; netstandard2.0 does not. We declare it ourselves as `internal` so it does not leak from the assembly's public surface.

- [ ] **Step 8.1: Create the polyfill file**

```csharp
#if NETSTANDARD2_0
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit
    {
    }
}
#endif
```

Save as `CoinMarketCapDotNet/Polyfills/IsExternalInit.cs` (the SDK glob will pick it up automatically).

- [ ] **Step 8.2: Smoke-test that `init` setters now compile on netstandard2.0**

Add this temporary file `CoinMarketCapDotNet/Polyfills/_InitSmokeTest.cs`:

```csharp
namespace CoinMarketCapDotNet.Polyfills
{
    internal sealed class _InitSmokeTest
    {
        public string Value { get; init; } = string.Empty;
    }
}
```

Build:

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release
```

Expected: succeeds for both TFMs. Now delete the smoke-test file:

```bash
rm CoinMarketCapDotNet/Polyfills/_InitSmokeTest.cs
```

- [ ] **Step 8.3: Commit**

```bash
git add CoinMarketCapDotNet/Polyfills/IsExternalInit.cs
git commit -m "Add IsExternalInit polyfill for netstandard2.0 to enable init-only setters"
```

---

## Task 9: Add typed exception hierarchy

**Files:**
- Create: `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapException.cs`
- Create: `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapBadRequestException.cs`
- Create: `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapAuthException.cs`
- Create: `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapRateLimitException.cs`
- Create: `CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapServerException.cs`
- Create: `CoinMarketCapDotNet_Tests/StubHandlers/StubHttpMessageHandler.cs`
- Create: `CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs`
- Modify: `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs` (replace raw `throw new Exception(...)` with typed throws)

This task uses TDD: stub HTTP handler → failing test → implementation → passing test.

- [ ] **Step 9.1: Create the base exception class**

`CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapException.cs`:

```csharp
using System;
using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public class CoinMarketCapException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public int? ErrorCode { get; }
        public string? CmcErrorMessage { get; }

        public CoinMarketCapException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            CmcErrorMessage = cmcErrorMessage;
        }

        public CoinMarketCapException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            CmcErrorMessage = cmcErrorMessage;
        }
    }
}
```

- [ ] **Step 9.2: Create the four specialized exceptions**

`CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapBadRequestException.cs`:

```csharp
using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapBadRequestException : CoinMarketCapException
    {
        public CoinMarketCapBadRequestException(int? errorCode, string? cmcErrorMessage, string message)
            : base(HttpStatusCode.BadRequest, errorCode, cmcErrorMessage, message) { }
    }
}
```

`CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapAuthException.cs`:

```csharp
using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapAuthException : CoinMarketCapException
    {
        public CoinMarketCapAuthException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(statusCode, errorCode, cmcErrorMessage, message) { }
    }
}
```

`CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapRateLimitException.cs`:

```csharp
using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapRateLimitException : CoinMarketCapException
    {
        public CoinMarketCapRateLimitException(int? errorCode, string? cmcErrorMessage, string message)
            : base((HttpStatusCode)429, errorCode, cmcErrorMessage, message) { }
    }
}
```

`CoinMarketCapDotNet/Models/Exceptions/CoinMarketCapServerException.cs`:

```csharp
using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapServerException : CoinMarketCapException
    {
        public CoinMarketCapServerException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(statusCode, errorCode, cmcErrorMessage, message) { }
    }
}
```

- [ ] **Step 9.3: Create the test stub handler**

`CoinMarketCapDotNet_Tests/StubHandlers/StubHttpMessageHandler.cs`:

```csharp
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoinMarketCapDotNet_Tests.StubHandlers
{
    internal sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _body;
        public HttpRequestMessage? LastRequest { get; private set; }
        public CancellationToken LastCancellationToken { get; private set; }

        public StubHttpMessageHandler(HttpStatusCode statusCode, string body)
        {
            _statusCode = statusCode;
            _body = body;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            LastCancellationToken = cancellationToken;
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_body)
            };
            return Task.FromResult(response);
        }
    }
}
```

- [ ] **Step 9.4: Write failing unit tests for typed exceptions**

Create `CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs`:

```csharp
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Models.Cryptocurrency.Map;
using CoinMarketCapDotNet.Models.Exceptions;
using CoinMarketCapDotNet.Models.General;
using CoinMarketCapDotNet_Tests.StubHandlers;
using Xunit;

namespace CoinMarketCapDotNet_Tests
{
    public class CoinMarketCapAPIUnitTests
    {
        private const string ErrorBody = """
        { "status": { "error_code": 1001, "error_message": "Test error" } }
        """;

        private static CoinMarketCapAPI ApiWithStub(HttpStatusCode statusCode, string body, out StubHttpMessageHandler handler)
        {
            handler = new StubHttpMessageHandler(statusCode, body);
            var client = new HttpClient(handler);
            return new CoinMarketCapAPI("test-key", client);
        }

        [Fact]
        public async Task BadRequest_throws_CoinMarketCapBadRequestException()
        {
            var api = ApiWithStub(HttpStatusCode.BadRequest, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapBadRequestException>(
                () => api.Cryptocurrency.GetMapAsync());
            Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.Equal(1001, ex.ErrorCode);
            Assert.Equal("Test error", ex.CmcErrorMessage);
        }

        [Fact]
        public async Task Unauthorized_throws_CoinMarketCapAuthException()
        {
            var api = ApiWithStub(HttpStatusCode.Unauthorized, ErrorBody, out _);
            var ex = await Assert.ThrowsAsync<CoinMarketCapAuthException>(
                () => api.Cryptocurrency.GetMapAsync());
            Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
        }

        [Fact]
        public async Task Forbidden_throws_CoinMarketCapAuthException()
        {
            var api = ApiWithStub(HttpStatusCode.Forbidden, ErrorBody, out _);
            await Assert.ThrowsAsync<CoinMarketCapAuthException>(
                () => api.Cryptocurrency.GetMapAsync());
        }

        [Fact]
        public async Task TooManyRequests_throws_CoinMarketCapRateLimitException()
        {
            var api = ApiWithStub((HttpStatusCode)429, ErrorBody, out _);
            await Assert.ThrowsAsync<CoinMarketCapRateLimitException>(
                () => api.Cryptocurrency.GetMapAsync());
        }

        [Fact]
        public async Task InternalServerError_throws_CoinMarketCapServerException()
        {
            var api = ApiWithStub(HttpStatusCode.InternalServerError, ErrorBody, out _);
            await Assert.ThrowsAsync<CoinMarketCapServerException>(
                () => api.Cryptocurrency.GetMapAsync());
        }

        [Fact]
        public async Task Ok_returns_deserialized_payload()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, okBody, out _);
            var result = await api.Cryptocurrency.GetMapAsync();
            Assert.NotNull(result);
        }
    }
}
```

Note: This test file references the new injectable-`HttpClient` constructor (`new CoinMarketCapAPI("test-key", client)`) that does not yet exist. The next task adds it. Right now the tests should fail to compile — that's the expected red state.

- [ ] **Step 9.5: Modify `CoinMarketCapAPI.cs` — add second-arg `HttpClient` overload AND replace exception throws**

In `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs`:

(a) Add `using CoinMarketCapDotNet.Models.Exceptions;` to the using directives.

(b) Change the `private static readonly HttpClient client = new HttpClient();` line to allow injection. Replace it with:

```csharp
        private static readonly HttpClient _defaultClient = new HttpClient();
        private readonly HttpClient _client;
```

(c) Replace the existing `public CoinMarketCapAPI(string apiKey, bool useSandbox = false)` constructor body. The new shape is:

```csharp
        public CoinMarketCapAPI(string apiKey, bool useSandbox = false)
            : this(apiKey, useSandbox, httpClient: null)
        {
        }

        public CoinMarketCapAPI(string apiKey, HttpClient? httpClient)
            : this(apiKey, useSandbox: false, httpClient: httpClient)
        {
        }

        private CoinMarketCapAPI(string apiKey, bool useSandbox, HttpClient? httpClient)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("apiKey must be provided.", nameof(apiKey));

            this.apiKey = apiKey;
            this.apiBase = useSandbox ? "https://sandbox-api.coinmarketcap.com/"
                                      : "https://pro-api.coinmarketcap.com/";
            _client = httpClient ?? _defaultClient;

            Cryptocurrency = new CryptocurrencyEndpoint(this);
            Fiat = new FiatEndpoint(this);
            Exchange = new ExchangeEndpoint(this);
            GlobalMetrics = new GlobalMetricsEndpoint(this);
            Tools = new ToolsEndpoint(this);
            Blockchain = new BlockchainEndpoint(this);
            Key = new KeyEndpoint(this);
            Content = new ContentEndpoint(this);
            Community = new CommunityEndpoint(this);
        }
```

(d) Inside `GetDataAsync<T>`, replace `await client.SendAsync(request)` with `await _client.SendAsync(request)`.

(e) Replace the entire `switch` block in `GetDataAsync<T>` with this typed-exception version:

```csharp
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadAsStringAsync();
                    T? result = JsonSerializer.Deserialize<T>(content, JsonOptions);
                    return result ?? throw new CoinMarketCapException(
                        HttpStatusCode.OK, null, null,
                        "Failed to deserialize response content.");

                case HttpStatusCode.BadRequest:
                    {
                        var (code, msg) = await ReadStatusAsync(response);
                        throw new CoinMarketCapBadRequestException(code, msg, $"Bad request: {msg}");
                    }

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    {
                        var (code, msg) = await ReadStatusAsync(response);
                        throw new CoinMarketCapAuthException(response.StatusCode, code, msg,
                            $"{response.StatusCode}: {msg}");
                    }

                case (HttpStatusCode)429:
                    {
                        var (code, msg) = await ReadStatusAsync(response);
                        throw new CoinMarketCapRateLimitException(code, msg, $"Rate limited: {msg}");
                    }

                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.GatewayTimeout:
                    {
                        var (code, msg) = await ReadStatusAsync(response);
                        throw new CoinMarketCapServerException(response.StatusCode, code, msg,
                            $"{response.StatusCode}: {msg}");
                    }

                default:
                    {
                        var (code, msg) = await ReadStatusAsync(response);
                        throw new CoinMarketCapException(response.StatusCode, code, msg,
                            $"Unexpected status {response.StatusCode}: {msg}");
                    }
            }
```

(f) Add the `ReadStatusAsync` helper method inside the `CoinMarketCapAPI` class (just below `GetDataAsync<T>`):

```csharp
        private static async Task<(int? code, string? message)> ReadStatusAsync(HttpResponseMessage response)
        {
            try
            {
                var body = await response.Content.ReadAsStringAsync();
                var parsed = JsonSerializer.Deserialize<ResponseDict<Status>>(body, JsonOptions);
                return (parsed?.Status?.ErrorCode, parsed?.Status?.ErrorMessage);
            }
            catch
            {
                return (null, null);
            }
        }
```

(The defensive `try/catch` covers the case where the error body is HTML or empty rather than JSON — the wrapper still throws a typed exception with the HTTP status code, just with null `ErrorCode`/`ErrorMessage`.)

(g) Verify `Status` model property names — open `CoinMarketCapDotNet/Models/General/Status.cs` and confirm the properties read `ErrorCode` and `ErrorMessage` (matching the helper above). If they are named differently (e.g. `error_code`), use the actual property names.

- [ ] **Step 9.6: Run the new unit tests — they should pass**

```bash
dotnet test CoinMarketCapDotNet.sln --filter "FullyQualifiedName~CoinMarketCapAPIUnitTests"
```

Expected: all six tests in `CoinMarketCapAPIUnitTests` pass.

If the OK-payload test fails with a deserialization error, inspect `CoinMarketCapDotNet/Models/Cryptocurrency/Map/MapData.cs` — the test response body has `data: []` and should deserialize cleanly into `Response<List<MapData>>` (or whatever the return type of `GetMapAsync` is). Adjust the test's `okBody` to match the real shape if needed.

- [ ] **Step 9.7: Commit**

```bash
git add CoinMarketCapDotNet/Models/Exceptions/ CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs CoinMarketCapDotNet_Tests/StubHandlers/ CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs
git commit -m "Add typed CoinMarketCapException hierarchy and HttpClient injection"
```

---

## Task 10: Add `CancellationToken` to every async method

**Files:**
- Modify: `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs` (add a trailing `CancellationToken` parameter to every `Task<...>` method on `CoinMarketCapAPI` and on each nested endpoint class).
- Modify: `CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs` (add a test confirming the token reaches the handler).

- [ ] **Step 10.1: Write a failing test for token propagation**

Append this `[Fact]` to `CoinMarketCapAPIUnitTests.cs`:

```csharp
        [Fact]
        public async Task CancellationToken_propagates_to_HttpClient()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var api = ApiWithStub(HttpStatusCode.OK, okBody, out var handler);
            using var cts = new System.Threading.CancellationTokenSource();
            await api.Cryptocurrency.GetMapAsync(cancellationToken: cts.Token);
            Assert.Equal(cts.Token, handler.LastCancellationToken);
        }
```

Run:

```bash
dotnet test CoinMarketCapDotNet.sln --filter "FullyQualifiedName~CancellationToken_propagates"
```

Expected: compilation error (`GetMapAsync` does not yet accept a `cancellationToken` parameter).

- [ ] **Step 10.2: Update `GetDataAsync<T>` signature**

In `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs`, change:

```csharp
public async Task<T> GetDataAsync<T>(string endpoint) where T : class
```

to:

```csharp
public async Task<T> GetDataAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : class
```

Change `await _client.SendAsync(request)` to:

```csharp
var response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
```

Replace each `await response.Content.ReadAsStringAsync()` (there are several) with:

```csharp
await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)
```

Note: `ReadAsStringAsync(CancellationToken)` exists on `net8.0` but NOT on `netstandard2.0`. Wrap conditionally:

```csharp
#if NET8_0_OR_GREATER
                    var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
```

Apply this `#if`/`#else`/`#endif` pattern to **every** `ReadAsStringAsync` call in the file (including the helper `ReadStatusAsync` — change its signature to accept `CancellationToken cancellationToken` and apply the same pattern, then update all callers in the `switch` block to pass the token through).

- [ ] **Step 10.3: Add `CancellationToken cancellationToken = default` to every endpoint method**

Every `public async Task<...>` method on every nested endpoint class (`CryptocurrencyEndpoint`, `FiatEndpoint`, `ExchangeEndpoint`, `GlobalMetricsEndpoint`, `ToolsEndpoint`, `BlockchainEndpoint`, `KeyEndpoint`, `ContentEndpoint`, `CommunityEndpoint`) gains a trailing `CancellationToken cancellationToken = default` parameter. The token is forwarded into `coinMarketCapAPI.GetDataAsync<...>(endpoint, cancellationToken)`.

Approach: open `CoinMarketCapAPI.cs`, search for `await coinMarketCapAPI.GetDataAsync<` and add `, cancellationToken` before the closing `)`. Then for each enclosing method signature, add the parameter.

Helper search to locate every method:

```bash
grep -n "public async Task" CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs
```

Expected count: ~60 methods (one per endpoint). Update each.

- [ ] **Step 10.4: Run the cancellation test — should now pass**

```bash
dotnet test CoinMarketCapDotNet.sln --filter "FullyQualifiedName~CoinMarketCapAPIUnitTests"
```

Expected: all seven unit tests pass on both TFMs of the library (test project itself is `net8.0`-only).

- [ ] **Step 10.5: Commit**

```bash
git add CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs
git commit -m "Add CancellationToken support to every async endpoint method"
```

---

## Task 11: Add `CoinMarketCapOptions` and the options-pattern constructor

**Files:**
- Create: `CoinMarketCapDotNet/Configuration/CoinMarketCapOptions.cs`
- Modify: `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs` (new constructor)
- Modify: `CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs` (add tests)

- [ ] **Step 11.1: Write failing tests for the options-pattern constructor**

Append to `CoinMarketCapAPIUnitTests.cs`:

```csharp
        [Fact]
        public void Options_constructor_validates_ApiKey()
        {
            var options = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions { ApiKey = "" };
            Assert.Throws<ArgumentException>(() => new CoinMarketCapAPI(options));
        }

        [Fact]
        public async Task Options_constructor_uses_BaseAddress_override()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var options = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions
            {
                ApiKey = "test-key",
                BaseAddress = new Uri("https://example.com/")
            };
            var api = new CoinMarketCapAPI(options, client);
            await api.Cryptocurrency.GetMapAsync();
            Assert.NotNull(handler.LastRequest);
            Assert.StartsWith("https://example.com/", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async Task Options_constructor_sandbox_mode_uses_sandbox_host()
        {
            const string okBody = """
            { "status": { "error_code": 0, "error_message": null }, "data": [] }
            """;
            var handler = new StubHttpMessageHandler(HttpStatusCode.OK, okBody);
            var client = new HttpClient(handler);
            var options = new CoinMarketCapDotNet.Configuration.CoinMarketCapOptions
            {
                ApiKey = "test-key",
                UseSandbox = true
            };
            var api = new CoinMarketCapAPI(options, client);
            await api.Cryptocurrency.GetMapAsync();
            Assert.StartsWith("https://sandbox-api.coinmarketcap.com/", handler.LastRequest!.RequestUri!.ToString());
        }
```

Run:

```bash
dotnet test CoinMarketCapDotNet.sln --filter "FullyQualifiedName~Options_constructor"
```

Expected: compilation errors (the type `CoinMarketCapOptions` does not yet exist).

- [ ] **Step 11.2: Create `CoinMarketCapOptions`**

`CoinMarketCapDotNet/Configuration/CoinMarketCapOptions.cs`:

```csharp
using System;

namespace CoinMarketCapDotNet.Configuration
{
    public sealed class CoinMarketCapOptions
    {
        public string ApiKey { get; init; } = string.Empty;

        public bool UseSandbox { get; init; }

        public Uri? BaseAddress { get; init; }

        public TimeSpan? Timeout { get; init; }
    }
}
```

- [ ] **Step 11.3: Add the options-pattern constructor to `CoinMarketCapAPI`**

In `CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs`, add `using CoinMarketCapDotNet.Configuration;` and add this constructor (place it just above the existing `public CoinMarketCapAPI(string apiKey, bool useSandbox = false)` constructor):

```csharp
        public CoinMarketCapAPI(CoinMarketCapOptions options, HttpClient? httpClient = null)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(options.ApiKey))
                throw new ArgumentException("ApiKey must be provided.", nameof(options));

            this.apiKey = options.ApiKey;
            if (options.BaseAddress is not null)
            {
                var raw = options.BaseAddress.ToString();
                this.apiBase = raw.EndsWith("/") ? raw : raw + "/";
            }
            else
            {
                this.apiBase = options.UseSandbox
                    ? "https://sandbox-api.coinmarketcap.com/"
                    : "https://pro-api.coinmarketcap.com/";
            }

            _client = httpClient ?? _defaultClient;
            if (options.Timeout is { } t && httpClient is null)
            {
                // Only mutate the default client's timeout if the consumer did not bring their own.
                // (Mutating an injected HttpClient would surprise the caller.)
                _defaultClient.Timeout = t;
            }

            Cryptocurrency = new CryptocurrencyEndpoint(this);
            Fiat = new FiatEndpoint(this);
            Exchange = new ExchangeEndpoint(this);
            GlobalMetrics = new GlobalMetricsEndpoint(this);
            Tools = new ToolsEndpoint(this);
            Blockchain = new BlockchainEndpoint(this);
            Key = new KeyEndpoint(this);
            Content = new ContentEndpoint(this);
            Community = new CommunityEndpoint(this);
        }
```

- [ ] **Step 11.4: Run tests — should pass**

```bash
dotnet test CoinMarketCapDotNet.sln --filter "FullyQualifiedName~CoinMarketCapAPIUnitTests"
```

Expected: all unit tests pass (including the three new options tests).

- [ ] **Step 11.5: Commit**

```bash
git add CoinMarketCapDotNet/Configuration/CoinMarketCapOptions.cs CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs CoinMarketCapDotNet_Tests/CoinMarketCapAPIUnitTests.cs
git commit -m "Add CoinMarketCapOptions and options-pattern constructor"
```

---

## Task 12: Enable nullable warnings as errors and resolve

**Files:**
- Modify: `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj`
- Modify: model files as warnings dictate (mass-annotate reference-type properties as nullable).

- [ ] **Step 12.1: Run the build to enumerate nullable warnings**

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release 2>&1 | grep -E "warning CS86|warning CS87" | sort -u | head -50
```

Capture the count:

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release 2>&1 | grep -E "warning CS86|warning CS87" | wc -l
```

Expected: a substantial number (every model property declared as a non-nullable reference type triggers a warning because the SDK never proves they are initialized).

- [ ] **Step 12.2: Annotate model properties as nullable**

For each warning of the form `CS8618: Non-nullable property '<Name>' must contain a non-null value when exiting constructor`, change the property type to nullable. For string properties:

```csharp
public string Name { get; set; }
```

becomes:

```csharp
public string? Name { get; set; }
```

For collection properties, prefer initializing them to an empty collection rather than nullable:

```csharp
public List<MapData> Data { get; set; } = new();
```

This is a mechanical pass. Run the build between batches to see remaining warnings shrink. There is no shortcut — each warning must be addressed because we will turn warnings into errors at the end of this task.

- [ ] **Step 12.3: Resolve warnings in `CoinMarketCapAPI.cs`**

Inside `GetDataAsync<T>`, the `JsonSerializer.Deserialize<T>(content, JsonOptions)` returns a nullable `T`. The OK case already throws on null. For the helper method `ReadStatusAsync`, the `parsed?.Status?.ErrorCode` chain is already null-safe.

Verify there are no remaining `CS86xx`/`CS87xx` warnings:

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release 2>&1 | grep -E "warning CS86|warning CS87"
```

Expected: no output.

- [ ] **Step 12.4: Turn warnings into errors**

Add `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` to the main `<PropertyGroup>` in `CoinMarketCapDotNet/CoinMarketCapDotNet.csproj`.

Build:

```bash
dotnet build CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release
```

Expected: succeeds with no warnings on either TFM. If new errors appear, fix them.

- [ ] **Step 12.5: Re-run all unit tests**

```bash
dotnet test CoinMarketCapDotNet.sln --filter "FullyQualifiedName~CoinMarketCapAPIUnitTests"
```

Expected: all unit tests still pass.

- [ ] **Step 12.6: Commit**

```bash
git add -A CoinMarketCapDotNet/Models CoinMarketCapDotNet/Api/CoinMarketCapAPI.cs CoinMarketCapDotNet/CoinMarketCapDotNet.csproj
git commit -m "Enable nullable reference types throughout and treat warnings as errors"
```

---

## Task 13: Update the test fixture to use the new constructor

**Files:**
- Modify: `CoinMarketCapDotNet_Tests/Fixture/CoinMarketCapAPIFixture.cs`

This task does not change behavior — it brings the integration-test fixture in line with the new constructor shape so it continues to compile.

- [ ] **Step 13.1: Replace the fixture with the options-pattern version**

Overwrite `CoinMarketCapDotNet_Tests/Fixture/CoinMarketCapAPIFixture.cs`:

```csharp
using System;
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;

namespace CoinMarketCapDotNet_Tests.Collection
{
    public class CoinMarketCapAPIFixture : IDisposable
    {
        public CoinMarketCapAPI CoinMarketCapAPI { get; private set; }
        private readonly string _apiKey = "your-valid-api-key";

        public CoinMarketCapAPIFixture()
        {
            CoinMarketCapAPI = new CoinMarketCapAPI(new CoinMarketCapOptions { ApiKey = _apiKey });
        }

        public void SetSandboxMode(bool useSandbox)
        {
            CoinMarketCapAPI = new CoinMarketCapAPI(new CoinMarketCapOptions
            {
                ApiKey = _apiKey,
                UseSandbox = useSandbox
            });
        }

        public void Dispose()
        {
            CoinMarketCapAPI = new CoinMarketCapAPI(new CoinMarketCapOptions { ApiKey = _apiKey });
        }
    }
}
```

- [ ] **Step 13.2: Build the solution**

```bash
dotnet build CoinMarketCapDotNet.sln -c Release
```

Expected: succeeds. The integration tests in `CoinMarketCapAPITests.cs` continue to compile because they only consume the fixture's `CoinMarketCapAPI` property — they do not construct it directly.

- [ ] **Step 13.3: Commit**

```bash
git add CoinMarketCapDotNet_Tests/Fixture/CoinMarketCapAPIFixture.cs
git commit -m "Update test fixture to use CoinMarketCapOptions constructor"
```

---

## Task 14: Write `MIGRATION-V1-TO-V2.md`

**Files:** Create `MIGRATION-V1-TO-V2.md` at the repo root.

- [ ] **Step 14.1: Create the migration guide**

Save as `MIGRATION-V1-TO-V2.md`:

````markdown
# Migrating from CoinMarketCapDotNet v1 to v2

v2 is a focused modernization release. The wire-level behavior is unchanged — every endpoint returns the same response shape it did in v1. What changed is the framework targets, the JSON dependency, and the public method signatures.

## What changed and why

- **Target frameworks:** v1 targeted only `.NET Framework 4.7.2`. v2 multi-targets `netstandard2.0` and `net8.0`. .NET Framework 4.6.1+, Mono, Unity, and any .NET Core 2.0+/.NET 5+ runtime can install v2.
- **JSON serializer:** Newtonsoft.Json has been replaced by `System.Text.Json`.
- **Runtime dependencies:** zero on `.NET 8`; a single `System.Text.Json` package on `netstandard2.0`.
- **API surface:** typed exceptions, `CancellationToken` on every async method, options-pattern configuration, injectable `HttpClient`, nullable reference types annotated.

## Framework support matrix

| Consumer runtime         | v1 (.NET Framework 4.7.2) | v2 (netstandard2.0;net8.0) |
|--------------------------|---------------------------|----------------------------|
| .NET Framework 4.6.0     | not supported             | not supported              |
| .NET Framework 4.6.1+    | not supported             | supported (via netstandard2.0) |
| .NET Framework 4.7.2+    | supported                 | supported (via netstandard2.0) |
| .NET Core 2.0+ / .NET 5+ | not supported             | supported                  |
| .NET 8                   | not supported             | supported (zero deps)      |
| Unity 2021.2+            | not supported             | supported (via netstandard2.1 implicit fallback) |

## Dependency changes

- `Newtonsoft.Json` is no longer a dependency. If your application only depended on it transitively through this package, you can remove it from your own `csproj`.
- On `.NET 8`, the package has zero NuGet dependencies — `System.Text.Json` is part of the shared framework.
- On `netstandard2.0`, the package brings `System.Text.Json` 8.x as its only dependency.

## Constructor migration

### v1 — simple case

```csharp
var api = new CoinMarketCapAPI("YOUR_API_KEY");
var sandbox = new CoinMarketCapAPI("YOUR_API_KEY", useSandbox: true);
```

### v2 — simple case still works

```csharp
var api = new CoinMarketCapAPI("YOUR_API_KEY");
var sandbox = new CoinMarketCapAPI("YOUR_API_KEY", useSandbox: true);
```

### v2 — options pattern (recommended)

```csharp
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;

var api = new CoinMarketCapAPI(new CoinMarketCapOptions
{
    ApiKey = "YOUR_API_KEY",
    UseSandbox = false,
    Timeout = TimeSpan.FromSeconds(30)
});
```

### v2 — with `IHttpClientFactory` (ASP.NET Core)

```csharp
// Program.cs
builder.Services.AddHttpClient("CoinMarketCap");
builder.Services.AddSingleton(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return new CoinMarketCapAPI(
        new CoinMarketCapOptions { ApiKey = builder.Configuration["CMC:ApiKey"]! },
        factory.CreateClient("CoinMarketCap"));
});
```

## Exception handling migration

### v1

```csharp
try
{
    var data = await api.Cryptocurrency.GetMapAsync();
}
catch (Exception ex)
{
    // Caller had no way to distinguish auth failures from rate limits from server errors.
    Console.WriteLine(ex.Message);
}
```

### v2

```csharp
using CoinMarketCapDotNet.Models.Exceptions;

try
{
    var data = await api.Cryptocurrency.GetMapAsync();
}
catch (CoinMarketCapAuthException ex)
{
    // 401 / 403 — bad or expired API key.
}
catch (CoinMarketCapRateLimitException ex)
{
    // 429 — back off.
}
catch (CoinMarketCapBadRequestException ex)
{
    // 400 — bad query parameters.
}
catch (CoinMarketCapServerException ex)
{
    // 5xx — transient. Retry-with-backoff is the consumer's responsibility.
}
catch (CoinMarketCapException ex)
{
    // Catch-all for any other CMC failure (preserves StatusCode, ErrorCode, CmcErrorMessage).
}
```

All typed exceptions derive from `CoinMarketCapException`, which derives from `System.Exception`. Old `catch (Exception)` blocks continue to work.

## CancellationToken

Every async method now accepts an optional `CancellationToken` as its last parameter:

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var data = await api.Cryptocurrency.GetMapAsync(cancellationToken: cts.Token);
```

## JSON payload shape

Response model property names and types are unchanged. Code that reads `Response<T>.Data.Whatever` continues to work as-is. Internally the deserialization is now case-insensitive, lenient about quoted numbers, and uses `System.Text.Json`'s `JsonStringEnumConverter` for enum-shaped fields — the same tolerances Newtonsoft provided.

If you wrote a custom `JsonConverter<T>` against Newtonsoft and applied it to a model in this library: rewrite it against `System.Text.Json.Serialization.JsonConverter<T>` and re-attach via `[JsonConverter(typeof(YourConverter))]`. The shape of the converter API is similar but not identical.

## Common pitfalls

- **Casing:** the deserializer is case-insensitive by default in v2. If you previously relied on case-sensitive failure to detect bad payloads, configure your own `JsonSerializerOptions` and use `JsonSerializer.Deserialize` directly on the response data.
- **Enums:** v2 deserializes enums from their string names (e.g. `"USD"` → `CurrencyEnum.USD`) using `JsonStringEnumConverter`. Numeric enum representations are not supported by default.
- **Timeouts:** the v1 wrapper used the static `HttpClient`'s default timeout (100 seconds). v2 still defaults to that but lets you override via `CoinMarketCapOptions.Timeout` (only when you do not bring your own `HttpClient`).
````

- [ ] **Step 14.2: Commit**

```bash
git add MIGRATION-V1-TO-V2.md
git commit -m "Add v1-to-v2 migration guide"
```

---

## Task 15: Polish `README.md`

**Files:** Modify `README.md`.

- [ ] **Step 15.1: Restructure the README**

Open `README.md`. The current order is: Known Issues → Release Notes → CoinMarketCapDotNet (overview) → Installation → Usage → endpoint tables → Extensions. Restructure to:

1. **CoinMarketCapDotNet** (overview) — keep as-is from line 46 onward.
2. **Migrating from v1?** — one-line section pointing to `MIGRATION-V1-TO-V2.md`.
3. **Installation** — update to `2.x` family; mention multi-target reach.
4. **Usage** — replace v1 constructor sample with both the simple v2 form and the options-pattern form. Add a typed-exception example.
5. **Endpoints** — keep existing tables.
6. **Extensions** — keep existing examples.
7. **Release Notes** — replace inline changelog with a one-line link to GitHub Releases. Keep only the v2.0.0 highlights inline.
8. **Known Issues** — move to the very bottom (it was at the top in v1).

Update the **Usage** section to replace the v1 sample (currently lines 79-86) with:

````markdown
## Usage

### Quick start

```csharp
using CoinMarketCapDotNet.Api;

var api = new CoinMarketCapAPI("YOUR_API_KEY");
var data = await api.Cryptocurrency.GetMapAsync();
```

### Options pattern (recommended for production)

```csharp
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;

var api = new CoinMarketCapAPI(new CoinMarketCapOptions
{
    ApiKey = "YOUR_API_KEY",
    UseSandbox = false,
    Timeout = TimeSpan.FromSeconds(30)
});

using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var data = await api.Cryptocurrency.GetMapAsync(cancellationToken: cts.Token);
```

### Error handling

```csharp
using CoinMarketCapDotNet.Models.Exceptions;

try
{
    var data = await api.Cryptocurrency.GetMapAsync();
}
catch (CoinMarketCapRateLimitException)  { /* back off */ }
catch (CoinMarketCapAuthException)       { /* check your API key */ }
catch (CoinMarketCapException ex)        { /* logs ex.StatusCode, ex.ErrorCode, ex.CmcErrorMessage */ }
```
````

Add this **Migrating from v1?** section directly above **Installation**:

```markdown
## Migrating from v1?

See [MIGRATION-V1-TO-V2.md](MIGRATION-V1-TO-V2.md) for the full v1 → v2 migration guide.
```

Replace the **Release Notes** block (currently lines 6-44) with:

```markdown
## Release Notes

### v2.0.0
- Multi-targets `netstandard2.0` and `net8.0`.
- Migrated to `System.Text.Json` — zero runtime dependencies on .NET 8.
- Typed exception hierarchy under `CoinMarketCapDotNet.Models.Exceptions`.
- `CancellationToken` on every async method.
- Options-pattern configuration via `CoinMarketCapOptions`.
- Injectable `HttpClient` for `IHttpClientFactory` / DI scenarios.
- See [MIGRATION-V1-TO-V2.md](MIGRATION-V1-TO-V2.md) for upgrade instructions.

For older v1.x release notes, see the [GitHub Releases page](https://github.com/msanlisavas/CoinMarketCapDotNet/releases).
```

Move the **Known Issues** block (currently lines 1-4) to the very bottom of the file, under a new `## Known Issues` heading.

- [ ] **Step 15.2: Commit**

```bash
git add README.md
git commit -m "Polish README for v2: restructure, add migration link, update usage samples"
```

---

## Task 16: Final verification — multi-target build, pack, smoke

**Files:** none modified. Verification only.

- [ ] **Step 16.1: Clean rebuild on both target frameworks**

```bash
dotnet clean CoinMarketCapDotNet.sln
dotnet build CoinMarketCapDotNet.sln -c Release --no-incremental
```

Expected: succeeds with no warnings (warnings-as-errors is on for the library).

- [ ] **Step 16.2: Run all unit tests**

```bash
dotnet test CoinMarketCapDotNet.sln -c Release --filter "FullyQualifiedName~CoinMarketCapAPIUnitTests"
```

Expected: all unit tests pass. Integration tests in `CoinMarketCapAPITests.cs` are not run automatically (they require a real API key in the fixture).

- [ ] **Step 16.3: Pack the NuGet package**

```bash
rm -rf artifacts
dotnet pack CoinMarketCapDotNet/CoinMarketCapDotNet.csproj -c Release -o ./artifacts
```

Expected:
- `artifacts/CoinMarketCapDotNet.2.0.0.nupkg` exists.
- `artifacts/CoinMarketCapDotNet.2.0.0.snupkg` exists.

- [ ] **Step 16.4: Inspect the packed nuspec**

```bash
unzip -p artifacts/CoinMarketCapDotNet.2.0.0.nupkg CoinMarketCapDotNet.nuspec
```

Verify in the output:
- `<id>CoinMarketCapDotNet</id>`
- `<version>2.0.0</version>`
- `<authors>Murat Sanlisavas</authors>`
- `<license type="expression">MIT</license>`
- `<readme>README.md</readme>`
- `<repository ... url="https://github.com/msanlisavas/CoinMarketCapDotNet" />`
- `<dependencies>` block has a `netstandard2.0` group with `System.Text.Json` and a `net8.0` group with no dependencies (or only framework-implicit ones).

- [ ] **Step 16.5: Inspect the package contents**

```bash
unzip -l artifacts/CoinMarketCapDotNet.2.0.0.nupkg
```

Verify:
- Both `lib/netstandard2.0/CoinMarketCapDotNet.dll` and `lib/net8.0/CoinMarketCapDotNet.dll` are present.
- Both `lib/netstandard2.0/CoinMarketCapDotNet.xml` and `lib/net8.0/CoinMarketCapDotNet.xml` are present (XML doc files).
- `README.md` is at the package root.

- [ ] **Step 16.6: Optional — install the package locally and smoke-test**

```bash
mkdir /tmp/cmc-smoke && cd /tmp/cmc-smoke
dotnet new console -n SmokeTest
cd SmokeTest
dotnet add package CoinMarketCapDotNet --source <repo-root>/artifacts --version 2.0.0
```

Replace `Program.cs` with:

```csharp
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;

var api = new CoinMarketCapAPI(new CoinMarketCapOptions { ApiKey = "demo" });
Console.WriteLine(api.GetType().AssemblyQualifiedName);
```

```bash
dotnet run
```

Expected: prints the assembly-qualified name including `Version=2.0.0.0`. (No HTTP call is made — `GetMapAsync()` would fail with `CoinMarketCapAuthException` because `"demo"` is not a real key.)

- [ ] **Step 16.7: Final commit (if anything changed) and tag**

If any verification step required a tweak, commit it with `git commit -m "Verification fixes for v2.0.0 release"`.

Tag the release (do **not** push — that is the user's call):

```bash
git tag v2.0.0
```

Verify the tag:

```bash
git log --oneline -1
git tag --list "v2*"
```

Expected: tag `v2.0.0` points at the most recent commit.

The plan is complete. The user pushes the commits and the tag when they are ready to publish.

---

## Spec coverage check

| Spec section | Implemented in |
|--------------|----------------|
| 1.1 Library SDK-style + multi-target + NuGet metadata | Tasks 2, 4 |
| 1.2 Tests SDK-style net8.0 + xUnit-only | Task 3 |
| 1.3 Directory.Build.props + Source Link + remove AssemblyInfo/packages.config | Tasks 2, 3, 4 |
| 2.1 Mechanical attribute rename | Task 5 |
| 2.2 Shared `JsonSerializerOptions` | Task 5 |
| 2.3 Custom converters as needed | Task 5 (Step 5.2 fallback paths) |
| 2.4 Validation against sandbox | Task 16 (unit tests pass; integration tests gated on user-provided key) |
| 3.1 Constructor — options pattern | Task 11 |
| 3.2 `CancellationToken` on every method | Task 10 |
| 3.3 Typed exception hierarchy | Task 9 |
| 3.4 Nullable reference types | Task 12 |
| 3.5 Namespace bug fix | Task 7 |
| 4.1 `MIGRATION-V1-TO-V2.md` | Task 14 |
| 4.2 README polish | Task 15 |
| 5 Tests project + drop MSTest + sandbox run | Tasks 3, 13, 16 |
| Risks: `IsExternalInit` polyfill | Task 8 |

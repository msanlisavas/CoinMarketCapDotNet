using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Content.Latest.Query
{
    public class ContentLatestQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start);
        }
        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }

        public void AddId(string ids)
        {
            Add("id", ids);
        }

        public void AddSlug(string slugs)
        {
            Add("slug", slugs);
        }

        public void AddSymbol(string symbols)
        {
            Add("symbol", symbols);
        }

        public void AddNewsType(NewsTypeEnum newsType)
        {
            Add("news_type", newsType.GetEnumMemberValue());
        }

        public void AddContentType(ContentTypeEnum contentType)
        {
            Add("content_type", contentType.GetEnumMemberValue());
        }

        public void AddCategory(string categories)
        {
            Add("category", categories);
        }

        public void AddLanguage(LanguageEnum language)
        {
            Add("language", language.GetEnumMemberValue());
        }
    }
}

using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Content.Posts.Latest.Query
{
    public class ContentPostsLatestQueryParameters : QueryParameters
    {
        public void AddId(string id)
        {
            Add("id", id);
        }
        public void AddSlug(string slug)
        {
            Add("slug", slug);
        }
        public void AddSymbol(string symbol)
        {
            Add("symbol", symbol);
        }
        public void AddLastScore(string lastScore)
        {
            Add("last_score", lastScore);
        }
    }
}

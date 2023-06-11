using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Content.Posts.Comments.Query
{
    public class PostCommentsQueryParameters : QueryParameters
    {
        public void AddPostId(string postId)
        {
            Add("post_id", postId);
        }
    }
}

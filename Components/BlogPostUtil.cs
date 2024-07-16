using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components {
    public static class BlogPostUtil {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns>The UrlHandle for the given BlogPost</returns>
        public static string GetURLHandle(BlogPost blogPost) => $"/Blogs/{blogPost.UrlHandle}";
    }
}

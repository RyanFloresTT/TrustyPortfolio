using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components
{
    public static class ProjectUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>The Project's URL Handle, given a Blog Post</returns>
        public static string GetProjetHandleURL(BlogPost blog) => $"/Blogs/{blog.UrlHandle}";
    }
}

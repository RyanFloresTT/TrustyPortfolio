using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class FeaturedBlogCard {
        [Parameter] public BlogPost BlogPost { get; set; }
        string GetURLHandle() => $"/Blogs/{BlogPost.UrlHandle}";
    }
}

using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Pages.Admin {
    public partial class BlogEditIndex {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public string UrlHandle {  get; set; }
        public BlogPost? BlogPost { get; set; }

        protected override async Task OnInitializedAsync() {
            BlogPost = PortfolioData.Blogs.FirstOrDefault(x => x.UrlHandle == UrlHandle);
            await base.OnInitializedAsync();
        }
    }
}

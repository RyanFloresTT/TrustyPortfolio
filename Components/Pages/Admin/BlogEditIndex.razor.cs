using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Admin {
    public partial class BlogEditIndex {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IBlogRepository BlogPostRepository { get; set; }
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public string UrlHandle {  get; set; }
        public BlogPost? BlogPost { get; set; }

        protected override async Task OnInitializedAsync() {
            BlogPost = PortfolioData.Blogs.FirstOrDefault(x => x.UrlHandle == UrlHandle);
            await base.OnInitializedAsync();
        }
        private async Task HandleSubmit() {
            try {
                await BlogPostRepository.UpdateAsync(BlogPost);
                NavigationManager.NavigateTo("/Admin/Blogs/Edit");
            }
            catch (Exception ex) {
                // Handle any errors during update (show error message)
            }
        }

    }
}

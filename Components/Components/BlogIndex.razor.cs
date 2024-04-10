using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Components {
    public partial class BlogIndex {
        [Parameter] public string UrlHandle { get; set; }
        [Inject] IBlogRepository BlogRepository { get; set; }
        public BlogPost BlogPost { get; set; }

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            BlogPost = await BlogRepository.GetByUrlHandleAsync(UrlHandle);
        }

    }
}

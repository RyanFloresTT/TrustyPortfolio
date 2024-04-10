using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Components {
    public partial class ProjectIndex {
        [Parameter] public string UrlHandle { get; set; }
        [Inject] IProjectRepository ProjectRepository { get; set; }
        public Project Project { get; set; }

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            Project = await ProjectRepository.GetByUrlHandleAsync(UrlHandle);
        }

    }
}

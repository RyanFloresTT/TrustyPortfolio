using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Admin {
    public partial class ProjectEditIndex {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IProjectRepository ProjectRepository { get; set; }
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public string UrlHandle {  get; set; }
        public Project? Project { get; set; }

        protected override async Task OnInitializedAsync() {
            Project = PortfolioData.Projects.FirstOrDefault(x => x.UrlHandle == UrlHandle);
            await base.OnInitializedAsync();
        }
        private async Task HandleSubmit() {
            try {
                await ProjectRepository.UpdateAsync(Project);
                NavigationManager.NavigateTo("/Admin/Projects/Edit");
            }
            catch (Exception ex) {
                // Handle any errors during update (show error message)
            }
        }

    }
}

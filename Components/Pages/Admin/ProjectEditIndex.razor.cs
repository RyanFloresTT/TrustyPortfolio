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
        public List<Tag> Tags { get; set; }
        public Tag SelectedTag { get; set; } = new();
        public IEnumerable<Tag> SelectedTags { get; set; }
        DateTime? projectDate;

        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (PortfolioData != null) {
                Project = PortfolioData.Projects.FirstOrDefault(x => x.UrlHandle == UrlHandle);
                projectDate = Project.PublishDate;
                Tags = PortfolioData.Tags;
            }
            if (Project.Tags.Any()) {
                SelectedTags = Project.Tags;
            }
        }

        private async Task HandleSubmit() {
            try {
                if (SelectedTags.Any()) {
                    Project.Tags = new List<Tag>(); 
                    foreach (var tag in SelectedTags) {
                        if (tag != null) {
                            Project.Tags.Add(tag);
                        }
                    }
                }
                if (projectDate != null) { 
                    Project.PublishDate = (DateTime)projectDate;
                    Project.PublishDate = Project.PublishDate.ToUniversalTime();
                }
                NavigationManager.NavigateTo("/Admin/Projects/Edit");
                await ProjectRepository.UpdateAsync(Project);
            }
            catch (Exception ex) {
                // Handle any errors during update (show error message)
            }
        }
        private string GetMultiSelectionText(List<string> selectedTags) {
            if (SelectedTags.Any()) {
                return string.Join(", ", SelectedTags.Select(x => x.Name));
            } else {
                return "No Tags Selected";
            }
        }

    }
}

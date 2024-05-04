using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Admin {
    public partial class ProjectCreate {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IProjectRepository ProjectRepository { get; set; }
        IList<IBrowserFile> files = new List<IBrowserFile>();

        public Project NewProject { get; set; } = new();
        public List<Tag> Tags { get; set; }
        public Tag SelectedTag { get; set; } = new();
        public IEnumerable<Tag> SelectedTags { get; set; }


        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (PortfolioData != null) {
                Tags = PortfolioData.Tags;
            }
        }

        async Task HandleSubmit() {
            try {
                if (SelectedTags.Any()) {
                    NewProject.Tags = new List<Tag>();
                    foreach (var tag in SelectedTags) {
                        if (tag != null) {
                            NewProject.Tags.Add(tag);
                        }
                    }
                }

                NewProject.PublishDate = NewProject.PublishDate.ToUniversalTime();
                await ProjectRepository.AddAsync(NewProject);
                NavigationManager.NavigateTo("/Admin/Projects/Edit");
            }
            catch (Exception ex) {
            }
        }
        void UploadFile(IBrowserFile file) {
            files.Add(file);
        }
    }
}
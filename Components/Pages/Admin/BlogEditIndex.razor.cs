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
        public List<Tag> Tags { get; set; }

        public List<Project> Projects { get; set; }
        public Tag SelectedTag { get; set; } = new();
        public IEnumerable<Tag> SelectedTags { get; set; }

        protected override async Task OnParametersSetAsync() {
            if (PortfolioData != null) {
                BlogPost = PortfolioData.Blogs.FirstOrDefault(x => x.UrlHandle == UrlHandle);
                Tags = PortfolioData.Tags;
                Projects = PortfolioData.Projects;
            }
            if (BlogPost.Tags.Any()) {
                SelectedTags = BlogPost.Tags;
            }
            await base.OnParametersSetAsync();
        }
        private async Task HandleSubmit() {
            try {
                if (SelectedTags.Any()) {
                    BlogPost.Tags = new List<Tag>();
                    foreach (var tag in SelectedTags) {
                        if (tag != null) {
                            BlogPost.Tags.Add(tag);
                        }
                    }
                }
                await BlogPostRepository.UpdateAsync(BlogPost);
                NavigationManager.NavigateTo("/Admin/Blogs/Edit");
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

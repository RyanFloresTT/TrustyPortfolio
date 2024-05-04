using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Admin {
    public partial class BlogCreate {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IBlogRepository BlogPostRepository { get; set; }
        IList<IBrowserFile> files = new List<IBrowserFile>();

        public BlogPost NewBlogPost { get; set; } = new();
        public List<Project> Projects { get; set; }
        public List<Tag> Tags { get; set; }
        public IEnumerable<Tag> SelectedTags { get; set; }
        public Tag SelectedTag { get; set; } = new();

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            if (PortfolioData != null) {
                Projects = PortfolioData.Projects;
                Tags = PortfolioData.Tags;
            }
        }

        async Task HandleSubmit() {
            try {
                NewBlogPost.PublishDate = NewBlogPost.PublishDate.ToUniversalTime();
                if (SelectedTags.Any()) {
                    NewBlogPost.Tags = new List<Tag>();
                    foreach (var tag in SelectedTags) {
                        if (tag != null) {
                            NewBlogPost.Tags.Add(tag);
                        }
                    }
                }
                
                await BlogPostRepository.AddAsync(NewBlogPost);
                NavigationManager.NavigateTo("/Admin/Blogs/Edit");
            }
            catch (Exception ex) {
            }
        }
        void UploadFile(IBrowserFile file) {
            files.Add(file);
        }
    }
}
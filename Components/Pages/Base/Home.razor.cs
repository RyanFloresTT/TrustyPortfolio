using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Base
{
    public partial class Home
    {
        [Inject] IBlogRepository BlogRepository { get; set; }
        [Inject] IProjectRepository ProjectRepository { get; set; }
        List<Project> featuredProjects = new();
        List<BlogPost> featuredBlogs = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var blogResult = await BlogRepository.GetFeaturedAsync();
            featuredBlogs = blogResult.ToList();

            var projectResult = await ProjectRepository.GetFeaturedAsync();
            featuredProjects = projectResult.ToList();
        }
    }
}
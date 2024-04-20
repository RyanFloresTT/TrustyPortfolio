using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Base
{
    public partial class ProjectList
    {
        [Inject] IProjectRepository ProjectRepository { get; set; }
        [Inject] ITagRepository TagRepository { get; set; }
        List<Project> projects = new();
        List<Tag> tags = new();
        List<Project> featuredProjects = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var result = await ProjectRepository.GetAllAsync("", "Publish Date", "Desc");
            projects = result.ToList();

            result = await ProjectRepository.GetFeaturedAsync();
            featuredProjects = result.ToList();

            var tagResult = await TagRepository.GetAllAsync();
            tags = tagResult.ToList();
        }
    }
}

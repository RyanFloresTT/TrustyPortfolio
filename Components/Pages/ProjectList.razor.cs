using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages {
    public partial class ProjectList {
        [Inject] IProjectRepository ProjectRepository { get; set; }
        List<Project> projects = new();
        List<Project> featuredProjects = new();

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            var result = await ProjectRepository.GetAllAsync("", "Publish Date", "Desc");
            projects = result.ToList();

            result = await ProjectRepository.GetFeaturedAsync();
            featuredProjects = result.ToList();
        }
    }
}

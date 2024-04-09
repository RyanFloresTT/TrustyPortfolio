using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages {
	public partial class Home {
		[Inject] IBlogRepository BlogRepository { get; set; }
		[Inject] ITagRepository TagRepository { get; set; }
		[Inject] IProjectRepository ProjectRepository { get; set; }
		List<Project> featuredProjects = new();
		List<BlogPost> featuredBlogs = new();
		List<Tag> allTags = new();

		protected override async Task OnInitializedAsync() {
			await base.OnInitializedAsync();

			var blogResult = await BlogRepository.GetFeaturedAsync();
			featuredBlogs = blogResult.ToList();

			var projectRresult = await ProjectRepository.GetFeaturedAsync();
			featuredProjects = projectRresult.ToList();

			var tagResult = await TagRepository.GetAllAsync();
			allTags = tagResult.ToList();
		}
	}
}
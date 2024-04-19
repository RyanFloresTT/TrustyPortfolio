using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
	public partial class FeaturedProjectsCarousel {
		[Parameter] public List<Project> Projects { get; set; }
		Transition Transition { get; set; } = Transition.Slide;

		private bool arrows = true;
		private bool bullets = false;
		private bool enableSwipeGesture = true;
		private bool autocycle = true;

		public List<string> GetFeaturedProjectNames() {
			List<string> projectNames = new();
			foreach (var project in Projects) {
				projectNames.Add(project.Title);
			}
			return projectNames;
		}

		string GetProjetHandleURL(Project project) => $"/Projects/{project.UrlHandle}";
	}
}

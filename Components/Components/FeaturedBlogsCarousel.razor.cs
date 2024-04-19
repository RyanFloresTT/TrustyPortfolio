using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
	public partial class FeaturedBlogsCarousel {
		[Parameter] public List<BlogPost> Blogs { get; set; }
		Transition Transition { get; set; } = Transition.Slide;

		private bool arrows = true;
		private bool bullets = false;
		private bool enableSwipeGesture = true;
		private bool autocycle = true;

		public List<string> GetFeaturedBlogNames() {
			List<string> blogNames = new();
			foreach (var blog in Blogs) {
				blogNames.Add(blog.Title);
			}
			return blogNames;
		}

		string GetProjetHandleURL(BlogPost blog) => $"/Blogs/{blog.UrlHandle}";
	}
}

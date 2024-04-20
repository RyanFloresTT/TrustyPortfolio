using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Base
{
    public partial class BlogList
    {
        [Inject] IBlogRepository BlogRepository { get; set; }
        [Inject] ITagRepository TagRepository { get; set; }
        List<BlogPost> blogs = new();
        List<BlogPost> featuredBlogs = new();
        List<Tag> tags = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var result = await BlogRepository.GetAllAsync("", "Publish Date", "Desc");
            blogs = result.ToList();

            result = await BlogRepository.GetFeaturedAsync();
            featuredBlogs = result.ToList();

            var tagResult = await TagRepository.GetAllAsync();
            tags = tagResult.ToList();
        }
    }
}

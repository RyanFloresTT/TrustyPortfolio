using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    public class AdminBlogPostsController(ITagRepository tagRepository, IBlogRepository blogPostRepository) : Controller {
        readonly ITagRepository tagRepository = tagRepository;
        readonly IBlogRepository blogPostRepository = blogPostRepository;

        [HttpGet]
        public async Task<IActionResult> Add() {
            // Get Tags from Repository
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest {
                Tags = tags.Select(x => new SelectListItem {
                    Text = x.DisplayName,
                    Value = x.Id.ToString(),
                })
            };
        
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest blogPostRequest) {

            var blogPost = new BlogPost {
                Heading = blogPostRequest.Heading,
                Title = blogPostRequest.Title,
                Content = blogPostRequest.Content,
                Description = blogPostRequest.Description,
                FeaturedImageUrl = blogPostRequest.FeaturedImageUrl,
                UrlHandle = blogPostRequest.UrlHandle,
                PublishDate = blogPostRequest.PublishDate,
                Visible = blogPostRequest.Visible,
            };

            var selectedTags = new List<Tag>();

            foreach (var tagId in blogPostRequest.SelectedTags) {
                var selectedTagId = Guid.Parse(tagId);
                var existingTag = await tagRepository.GetByGuidAsync(selectedTagId);

                if (existingTag != null) {
                    selectedTags.Add(existingTag);
                }
            }

            blogPost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }
    }
}

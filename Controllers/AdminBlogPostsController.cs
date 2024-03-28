using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public async Task<IActionResult> List() {
            var blogs = await blogPostRepository.GetAllAsync();

            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var blog = await blogPostRepository.GetByGuidAsync(id);
            var tags = await tagRepository.GetAllAsync();

            if (blog != null) {

                var model = new EditBlogPostRequest {
                    Id = blog.Id,
                    Heading = blog.Heading,
                    Title = blog.Title,
                    Content = blog.Content,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    UrlHandle = blog.UrlHandle,
                    Description = blog.Description,
                    PublishDate = blog.PublishDate,
                    Visible = blog.Visible,
                    Tags = tags.Select(x => new SelectListItem {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blog.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editRequest) {
            var blog = new BlogPost {
                Id = editRequest.Id,
                Title = editRequest.Title,
                Heading = editRequest.Heading,
                Content = editRequest.Content,
                FeaturedImageUrl = editRequest.FeaturedImageUrl,
                UrlHandle = editRequest.UrlHandle,
                Description = editRequest.Description,
                PublishDate = editRequest.PublishDate,
                Visible = editRequest.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editRequest.SelectedTags) {
                if (Guid.TryParse(selectedTag, out var tag)) {
                    var foundTag = await tagRepository.GetByGuidAsync(tag);
                    if (foundTag != null) {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blog.Tags = selectedTags;

            var updatedBlog = await blogPostRepository.UpdateAsync(blog);

            if (updatedBlog != null) {
                // Show Success Notification
                return RedirectToAction("Edit");
            } else {
                // Show Error Notification
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editRequest) {
            var deletedBlog = await blogPostRepository.DeleteAsync(editRequest.Id);

            if (deletedBlog != null) {
                // Show Success 
                return RedirectToAction("List");
            } else {
                // Show Error Notification
                return RedirectToAction("Edit", new { id = editRequest.Id });
            }
        }
    }
}

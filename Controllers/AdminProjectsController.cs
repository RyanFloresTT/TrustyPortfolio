using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminProjectsController(ITagRepository tagRepository, IProjectRepository projectRepository, IBlogRepository blogRepository) : Controller {
        readonly ITagRepository tagRepository = tagRepository;
        readonly IProjectRepository projectRepository = projectRepository;
        readonly IBlogRepository blogRepository = blogRepository;

        [HttpGet]
        public async Task<IActionResult> Add() {
            // Get Tags from Repository
            var tags = await tagRepository.GetAllAsync();

            var model = new AddProjectRequest {
                Tags = tags.Select(x => new SelectListItem {
                    Text = x.DisplayName,
                    Value = x.Id.ToString(),
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProjectRequest projectRequest) {

            var blogPost = new Project {
                Heading = projectRequest.Heading,
                Title = projectRequest.Title,
                Content = projectRequest.Content,
                Description = projectRequest.Description,
                FeaturedImageUrl = projectRequest.FeaturedImageUrl,
                ProjectUrl = projectRequest.ProjectUrl,
                UrlHandle = projectRequest.UrlHandle,
                PublishDate = projectRequest.PublishDate,
                Visible = projectRequest.Visible,
            };

            var selectedTags = new List<Tag>();

            foreach (var tagId in projectRequest.SelectedTags) {
                var selectedTagId = Guid.Parse(tagId);
                var existingTag = await tagRepository.GetByGuidAsync(selectedTagId);

                if (existingTag != null) {
                    selectedTags.Add(existingTag);
                }
            }

            blogPost.Tags = selectedTags;

            var selectedBlogs = new List<BlogPost>();

            foreach (var blogId in projectRequest.SelectedBlogs) {
                var selectedBlogId = Guid.Parse(blogId);
                var existingBlog = await blogRepository.GetByGuidAsync(selectedBlogId);

                if (existingBlog != null) { 
                    selectedBlogs.Add(existingBlog);
                }

            }

            blogPost.Blogs = selectedBlogs;

            await projectRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List() {
            var blogs = await projectRepository.GetAllAsync();

            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var blog = await projectRepository.GetByGuidAsync(id);
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

            var updatedBlog = await projectRepository.UpdateAsync(blog);

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
            var deletedBlog = await projectRepository.DeleteAsync(editRequest.Id);

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

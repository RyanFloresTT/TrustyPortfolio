using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController(ITagRepository tagRepository, IBlogRepository blogPostRepository, IProjectRepository projectRepository) : Controller {
        readonly ITagRepository tagRepository = tagRepository;
        readonly IBlogRepository blogPostRepository = blogPostRepository;
        readonly IProjectRepository projectRepository = projectRepository;

        [HttpGet]
        public async Task<IActionResult> Add() {
            // Get Tags from Repository
            var tags = await tagRepository.GetAllAsync();
            var projects = await projectRepository.GetAllAsync();

            var model = new AddBlogPostRequest {
                Tags = tags.Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }),
                Projects = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } }
                  .Concat(projects.Select(x => new SelectListItem {
                      Text = x.Title,
                      Value = x.Id.ToString(),
                  }))
                  .ToList()
            };
        
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest blogPostRequest) {

            var blogPost = new BlogPost {
                Title = blogPostRequest.Title,
                Content = blogPostRequest.Content,
                Description = blogPostRequest.Description,
                FeaturedImageUrl = blogPostRequest.FeaturedImageUrl,
                UrlHandle = blogPostRequest.UrlHandle,
                PublishDate = blogPostRequest.PublishDate,
                Visible = blogPostRequest.Visible,
                Featured = blogPostRequest.Featured,
            };

            var selectedProject = blogPostRequest.SelectedProjectId;

            if (selectedProject != null) {
                var projectId = Guid.Parse(selectedProject);
                var existingProject = await projectRepository.GetByGuidAsync(projectId);

                if (existingProject != null) {
                    blogPost.Project = existingProject;
                }
            }

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

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 10, int pageNumber = 1) {

            var totalTags = await blogPostRepository.CountAsync();

            var totalPages = Math.Ceiling((decimal)totalTags / pageSize);

            if (pageNumber > totalPages) {
                pageNumber = Convert.ToInt32(totalPages);
            }
            if (pageNumber < 1) {
                pageNumber = 1;
            }

            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;


            var tags = await blogPostRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var blog = await blogPostRepository.GetByGuidAsync(id);
            var tags = await tagRepository.GetAllAsync();
            var projects = await projectRepository.GetAllAsync();

            if (blog != null) {

                var model = new EditBlogPostRequest {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    UrlHandle = blog.UrlHandle,
                    Description = blog.Description,
                    PublishDate = blog.PublishDate,
                    Visible = blog.Visible,
                    Featured = blog.Featured,
                    Tags = tags.Select(x => new SelectListItem {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    Projects = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } }
                        .Concat(projects.Select(x => new SelectListItem {
                            Text = x.Title,
                            Value = x.Id.ToString(),
                        }))
                        .ToList(),
                    SelectedTags = blog.Tags.Select(x => x.Id.ToString()).ToArray(),
                    SelectedProjectId = blog.Project?.Id.ToString()
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
                Content = editRequest.Content,
                FeaturedImageUrl = editRequest.FeaturedImageUrl,
                UrlHandle = editRequest.UrlHandle,
                Description = editRequest.Description,
                PublishDate = editRequest.PublishDate,
                Visible = editRequest.Visible,
                Featured = editRequest.Featured,
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

            var selectedProject = editRequest.SelectedProjectId;

            if (selectedProject != null) {
                var projectId = Guid.Parse(selectedProject);
                var existingProject = await projectRepository.GetByGuidAsync(projectId);

                if (existingProject != null) {
                    blog.Project = existingProject;
                }
            }

            var updatedBlog = await blogPostRepository.UpdateAsync(blog);

            if (updatedBlog != null) {
                // Show Success Notification
            } else {
                // Show Error Notification
            }
            return RedirectToAction("List");
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

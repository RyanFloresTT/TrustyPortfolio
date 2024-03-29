using Microsoft.AspNetCore.Authorization;
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
            var blogs = await blogRepository.GetAllAsync();

            var model = new AddProjectRequest {
                Tags = tags.Select(x => new SelectListItem {
                    Text = x.DisplayName,
                    Value = x.Id.ToString(),
                }),
                Blogs = blogs.Select(x => new SelectListItem {
                    Text = x.Title,
                    Value = x.Id.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProjectRequest projectRequest) {

            var newProject = new Project {
                Heading = projectRequest.Heading,
                Title = projectRequest.Title,
                Content = projectRequest.Content,
                Description = projectRequest.Description,
                FeaturedImageUrl = projectRequest.FeaturedImageUrl,
                ProjectUrl = projectRequest.ProjectUrl,
                UrlHandle = projectRequest.UrlHandle,
                PublishDate = projectRequest.PublishDate,
                Visible = projectRequest.Visible,
                Featured = projectRequest.Featured,
            };

            var selectedTags = new List<Tag>();

            foreach (var tagId in projectRequest.SelectedTags) {
                var selectedTagId = Guid.Parse(tagId);
                var existingTag = await tagRepository.GetByGuidAsync(selectedTagId);

                if (existingTag != null) {
                    selectedTags.Add(existingTag);
                }
            }

            newProject.Tags = selectedTags;

            var selectedBlogs = new List<BlogPost>();

            foreach (var blogId in projectRequest.SelectedBlogs) {
                var selectedBlogId = Guid.Parse(blogId);
                var existingBlog = await blogRepository.GetByGuidAsync(selectedBlogId);

                if (existingBlog != null) { 
                    selectedBlogs.Add(existingBlog);
                }

            }

            newProject.Blogs = selectedBlogs;

            await projectRepository.AddAsync(newProject);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 3, int pageNumber = 1) {

            var totalTags = await projectRepository.CountAsync();

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


            var tags = await projectRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var project = await projectRepository.GetByGuidAsync(id);
            var tags = await tagRepository.GetAllAsync();
            var blogs = await blogRepository.GetAllAsync();

            if (project != null) {

                var model = new EditProjectRequest {
                    Id = project.Id,
                    Heading = project.Heading,
                    Title = project.Title,
                    Content = project.Content,
                    FeaturedImageUrl = project.FeaturedImageUrl,
                    UrlHandle = project.UrlHandle,
                    ProjectUrl = project.ProjectUrl,
                    Description = project.Description,
                    PublishDate = project.PublishDate,
                    Visible = project.Visible,
                    Featured = project.Featured,
                    Tags = tags.Select(x => new SelectListItem {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    Blogs = blogs.Select(x => new SelectListItem {
                        Text = x.Title,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = project.Tags.Select(x => x.Id.ToString()).ToArray(),
                    SelectedBlogs = project.Blogs.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectRequest editRequest) {
            var project = new Project {
                Id = editRequest.Id,
                Title = editRequest.Title,
                Heading = editRequest.Heading,
                Content = editRequest.Content,
                FeaturedImageUrl = editRequest.FeaturedImageUrl,
                ProjectUrl = editRequest.ProjectUrl,
                UrlHandle = editRequest.UrlHandle,
                Description = editRequest.Description,
                PublishDate = editRequest.PublishDate,
                Visible = editRequest.Visible,
                Featured = editRequest.Featured
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

            project.Tags = selectedTags;

            var updatedBlog = await projectRepository.UpdateAsync(project);

            if (updatedBlog != null) {
                // Show Success Notification
                return RedirectToAction("Edit");
            } else {
                // Show Error Notification
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditProjectRequest editRequest) {
            var deletedProject = await projectRepository.DeleteAsync(editRequest.Id);

            if (deletedProject != null) {
                // Show Success 
                return RedirectToAction("List");
            } else {
                // Show Error Notification
                return RedirectToAction("Edit", new { id = editRequest.Id });
            }
        }
    }
}

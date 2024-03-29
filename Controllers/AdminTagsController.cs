using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminTagsController(ITagRepository tagRepository) : Controller {
        readonly ITagRepository tagRepository = tagRepository;

        
        [HttpGet]
        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]    
        public async Task<IActionResult> Add(AddTagRequest tagRequest) {

            if (!ModelState.IsValid) {
                return View();
            }

            var tag = new Tag {
                Name = tagRequest.Name,
                DisplayName = tagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 3, int pageNumber = 1) {

            var totalTags = await tagRepository.CountAsync();

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


            var tags = await tagRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

            return View(tags); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var tag = await tagRepository.GetByGuidAsync(id);

            if (tag != null) {
                var editTag = new EditTagRequest {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTag);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editRequest) {
            var tag = new Tag {
                Id = editRequest.Id,
                Name = editRequest.Name,
                DisplayName = editRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null) {
                ViewBag.SuccessMessage = "Tag updated successfully!";
            } else {
                ViewBag.ErrorMessage = "Failed to update tag.";
            }

            return RedirectToAction("List", new { id = editRequest.Id });
        }

        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) {
            var tagToDelete = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (tagToDelete != null) {
                // Show Succss Notification
                return RedirectToAction("List");
            } else { 
                // Shoe Error Notification
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}

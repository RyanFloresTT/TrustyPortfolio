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

            var tag = new Tag {
                Name = tagRequest.Name,
                DisplayName = tagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() {
            
            var tags = await tagRepository.GetAllAsync();

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
                // Show Success Notification
            } else {
                // Show Error Notification
            }

            return RedirectToAction("Edit", new { id = editRequest.Id });
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

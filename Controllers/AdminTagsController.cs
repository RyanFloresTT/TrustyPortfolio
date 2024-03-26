using Microsoft.AspNetCore.Mvc;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    public class AdminTagsController(ITagRepository tagRepository) : Controller {
        readonly ITagRepository tagRepo = tagRepository;

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

            await tagRepo.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() {
            
            var tags = await tagRepo.GetAllAsync();

            return View(tags); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var tag = await tagRepo.GetByGuidAsync(id);

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

            var updatedTag = await tagRepo.UpdateAsync(tag);

            if (updatedTag != null) {
                // Show Success Notification
            } else {
                // Show Error Notification
            }

            return RedirectToAction("Edit", new { id = editRequest.Id });
        }

        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) {
            var tagToDelete = await tagRepo.DeleteAsync(editTagRequest.Id);

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

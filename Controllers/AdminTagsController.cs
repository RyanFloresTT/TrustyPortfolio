using Microsoft.AspNetCore.Mvc;
using TrustyPortfolio.Data;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;

namespace TrustyPortfolio.Controllers {
    public class AdminTagsController : Controller {
        readonly PortfolioDbContext db;
        public AdminTagsController(PortfolioDbContext dbContext) {
            db = dbContext;
        }

        [HttpGet]
        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]    
        public IActionResult Add(AddTagRequest tagRequest) {

            var tag = new Tag {
                Name = tagRequest.Name,
                DisplayName = tagRequest.DisplayName
            };

            db.Tags.Add(tag);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult List() {
            
            var tags = db.Tags.ToList();

            return View(); 
        }
    }
}

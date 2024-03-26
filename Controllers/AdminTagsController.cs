using Microsoft.AspNetCore.Mvc;

namespace TrustyPortfolio.Controllers {
    public class AdminTagsController : Controller {
        [HttpGet]
        public IActionResult Add() {
            return View();
        }
    }
}

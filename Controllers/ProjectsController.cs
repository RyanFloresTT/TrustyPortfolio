using Microsoft.AspNetCore.Mvc;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    public class ProjectsController (IProjectRepository projectRepository) : Controller {
        readonly IProjectRepository projectRepository = projectRepository;

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle) {
            var project = await projectRepository.GetByUrlHandleAsync(urlHandle);
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> List() {
            var project = await projectRepository.GetAllAsync("", "Publish Date", "Desc");
            return View(project);
        }
    }
}

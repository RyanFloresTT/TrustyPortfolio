using Microsoft.AspNetCore.Mvc;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    public class BlogsController (IBlogRepository blogRepository) : Controller {
        readonly IBlogRepository blogRepository = blogRepository;
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle) {
            var blogPost = await blogRepository.GetByUrlHandleAsync(urlHandle);
            return View(blogPost);
        }
    }
}

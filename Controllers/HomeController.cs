using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrustyPortfolio.Models;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    public class HomeController(ILogger<HomeController> logger, IBlogRepository blogPostRepository) : Controller {
        readonly ILogger<HomeController> _logger = logger;
        readonly IBlogRepository blogPostRepository = blogPostRepository;

        public async Task<IActionResult> Index() {
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

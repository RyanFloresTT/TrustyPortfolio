using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrustyPortfolio.Models;
using TrustyPortfolio.Models.ViewModels;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    public class HomeController(ILogger<HomeController> logger, IBlogRepository blogPostRepository, ITagRepository tagRepository) : Controller {
        readonly ILogger<HomeController> _logger = logger;
        readonly IBlogRepository blogPostRepository = blogPostRepository;
        readonly ITagRepository tagRepository = tagRepository;

        public async Task<IActionResult> Index() {
            var blogPosts = await blogPostRepository.GetAllAsync();
            var tags = await tagRepository.GetAllAsync();

            var homeView = new HomeViewModel {
                BlogPosts = blogPosts,
                Tags = tags
            };

            return View(homeView);
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

using Microsoft.AspNetCore.Mvc;

namespace TrustyPortfolio.Controllers {
    public class AboutController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

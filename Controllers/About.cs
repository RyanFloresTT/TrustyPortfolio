using Microsoft.AspNetCore.Mvc;

namespace TrustyPortfolio.Controllers {
    public class About : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

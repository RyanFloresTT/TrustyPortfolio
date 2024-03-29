using Microsoft.AspNetCore.Mvc;

namespace TrustyPortfolio.Controllers {
    public class ResumeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

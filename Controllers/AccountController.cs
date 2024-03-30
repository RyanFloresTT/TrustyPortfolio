using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrustyPortfolio.Models.ViewModels;

namespace TrustyPortfolio.Controllers {
    public class AccountController (SignInManager<IdentityUser> signInManager) : Controller {
        readonly SignInManager<IdentityUser> signInManager = signInManager;

        [HttpGet]
        public IActionResult Login(string returnUrl) {
            var model = new LoginViewModel {
                ReturnURL = returnUrl,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginRequest) {

            if (!ModelState.IsValid) {
                // Show error notification
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);

            if (result.Succeeded) {
                // show success notification
                if (!string.IsNullOrWhiteSpace(loginRequest.ReturnURL)) {
                    return RedirectToPage(loginRequest.ReturnURL);  
                }
                return RedirectToAction("Index", "Home");
            }

            // Show error notification
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied() { return View(); }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TrustyPortfolio.Models.ViewModels;

namespace TrustyPortfolio.Controllers {
    public class AccountController (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : Controller {
        readonly UserManager<IdentityUser> userManager = userManager;
        readonly SignInManager<IdentityUser> signInManager = signInManager;

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerRequest) {

            if (ModelState.IsValid) {
                var newUser = new IdentityUser {
                    UserName = registerRequest.Username,
                    Email = registerRequest.Email,
                };
                var userResult = await userManager.CreateAsync(newUser, registerRequest.Password);

                if (userResult.Succeeded) {
                    var roleAssignedUesr = await userManager.AddToRoleAsync(newUser, "User");
                    if (roleAssignedUesr.Succeeded) {
                        // Show success notification
                        return RedirectToAction("Register");
                    }
                }
            }
            
            // Show error notification
            return View();
        }

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

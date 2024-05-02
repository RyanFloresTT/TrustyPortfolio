using Microsoft.AspNetCore.Identity;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Utilities {
    public static class ManualEmailConfirmation {
        public static async Task ConfirmUserEmail(IServiceProvider serviceProvider, string email) {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync(email);
            if (user != null) {
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
            } else {
                throw new InvalidOperationException($"User with email '{email}' not found.");
            }
        }
    }
}

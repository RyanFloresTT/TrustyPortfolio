using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrustyPortfolio.Data {
    public class AuthDbContext : IdentityDbContext {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {

        }
    }
}

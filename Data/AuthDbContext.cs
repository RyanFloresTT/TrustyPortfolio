using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrustyPortfolio.Data {
    public class AuthDbContext : IdentityDbContext {
        public AuthDbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            var adminRoleId = "abe26ce0-577e-4b84-80f8-53d5c7461137";
            var superAdminRoleId = "fbce1eb3-5b53-4804-9847-b01a2b0c2006";
            var userRoleId = "81f53028-f4ce-48b5-8fc2-c5c0afffc4a7";

            var roles = new List<IdentityRole> {
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole{
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var email = "rryanflorres@gmail.com";
            var superAdminId = "f2d56cd8-184d-4ba0-b25a-34f54a51b7c2";

            var superAdminUser = new IdentityUser {
                UserName = email,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = email.ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "superAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            var superAdminRoles = new List<IdentityUserRole<string>> {
                new IdentityUserRole<string> {
                    RoleId = userRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string> {
                    RoleId = adminRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string> {
                    RoleId = superAdminRoleId,
                    UserId= superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}

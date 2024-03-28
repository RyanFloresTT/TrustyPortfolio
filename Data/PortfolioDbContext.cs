using Microsoft.EntityFrameworkCore;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Data {
    public class PortfolioDbContext : DbContext {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) {
        }
        
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}

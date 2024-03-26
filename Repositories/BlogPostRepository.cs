using TrustyPortfolio.Data;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Repositories {
    public class BlogPostRepository(PortfolioDbContext db) : IBlogRepository {
        private readonly PortfolioDbContext db = db;

        public async Task<BlogPost> AddAsync(BlogPost blogPost) {
            await db.AddAsync(blogPost);
            await db.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync() {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetByGuidAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost) {
            throw new NotImplementedException();
        }
    }
}

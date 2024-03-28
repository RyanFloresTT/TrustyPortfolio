using Microsoft.EntityFrameworkCore;
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

        public async Task<BlogPost?> DeleteAsync(Guid id) {
            var blog = await db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (blog != null) {
                db.BlogPosts.Remove(blog);
                await db.SaveChangesAsync();
                return blog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync() {
            return await db.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetByGuidAsync(Guid id) {
            return await db.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle) {
            return await db.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x =>x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost) {
            var existingBlog = await db.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null) {
                existingBlog.Id = blogPost.Id;
                existingBlog.Title = blogPost.Title;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.Content = blogPost.Content;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Description = blogPost.Description;
                existingBlog.PublishDate = blogPost.PublishDate;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await db.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}

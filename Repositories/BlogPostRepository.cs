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
        public async Task<int> CountAsync() {
            return await db.Tags.CountAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1, 
            int pageSize = 100
            ) {

            var query = db.BlogPosts.AsQueryable();

            // Filter
            if (!string.IsNullOrWhiteSpace(searchQuery)) {
                query = query.Where(x => x.Title.Contains(searchQuery));
            }

            // Sort
            if (!string.IsNullOrWhiteSpace(sortBy)) {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase)) {
                    query = isDesc ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            return await query.Include(x => x.Tags)
                                .Include(x => x.Project)
                                .ToListAsync();
        }

        public async Task<BlogPost?> GetByGuidAsync(Guid id) {
            return await db.BlogPosts.Include(x => x.Tags)
                                .Include(x => x.Project)
                                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle) {
            return await db.BlogPosts
                .Include(x => x.Tags)
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x =>x.UrlHandle == urlHandle);
        }

        public async Task<IEnumerable<BlogPost>> GetFeaturedAsync() {
            return await db.BlogPosts.Include(x => x.Tags)
                                    .Include(x => x.Project)
                                    .Where(x => x.Featured)
                                    .ToListAsync();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost) {
            var existingBlog = await db.BlogPosts.Include(x => x.Tags)
                                                .Include(x => x.Project)
                                                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null) {
                existingBlog.Id = blogPost.Id;
                existingBlog.Title = blogPost.Title;
                existingBlog.Content = blogPost.Content;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Description = blogPost.Description;
                existingBlog.PublishDate = blogPost.PublishDate;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Featured = blogPost.Featured;
                existingBlog.Tags = blogPost.Tags;
                existingBlog.Project = blogPost.Project;

                await db.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}

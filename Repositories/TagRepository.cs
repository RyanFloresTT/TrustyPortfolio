using Microsoft.EntityFrameworkCore;
using TrustyPortfolio.Data;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Models.ViewModels;

namespace TrustyPortfolio.Repositories {
    public class TagRepository(PortfolioDbContext dbContext) : ITagRepository {
        readonly PortfolioDbContext db = dbContext;

        public async Task<Tag> AddAsync(Tag tag) {
            await db.Tags.AddAsync(tag);
            await db.SaveChangesAsync();
            return tag;
        }

        public async Task<int> CountAsync() {
            return await db.Tags.CountAsync();
        }

        public async Task<Tag?> DeleteAsync(Guid id) {
            var tagToDelete = await db.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tagToDelete != null) {
                db.Tags.Remove(tagToDelete);
                await db.SaveChangesAsync();

                return tagToDelete;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1,
            int pageSize = 100
            ) {
            
            var query = db.Tags.AsQueryable();

            // Filter
            if (!string.IsNullOrWhiteSpace(searchQuery)) {
                query = query.Where(x => x.Name.Contains(searchQuery) || 
                                        x.DisplayName.Contains(searchQuery));
            }
            
            // Sort
            if (!string.IsNullOrWhiteSpace(sortBy)) {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase)) {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }
                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase)) {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Tag?> GetByGuidAsync(Guid id) {
            return await db.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag) {
            var existingTag = await db.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);

            if (existingTag != null) {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await db.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}

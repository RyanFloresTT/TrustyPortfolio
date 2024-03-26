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

        public async Task<Tag?> DeleteAsync(Guid id) {
            var tagToDelete = await db.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tagToDelete != null) {
                db.Tags.Remove(tagToDelete);
                await db.SaveChangesAsync();

                return tagToDelete;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync() {
            var tags = await db.Tags.ToListAsync();
            return tags;
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

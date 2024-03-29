using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Repositories {
    public interface IBlogRepository {
        Task<IEnumerable<BlogPost>> GetAllAsync(string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1,
            int pageSize = 100);
        Task<BlogPost?> GetByGuidAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
        Task<IEnumerable<BlogPost>> GetFeaturedAsync();
        Task<int> CountAsync();
    }
}

using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Repositories {
    public interface IBlogRepository {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByGuidAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}

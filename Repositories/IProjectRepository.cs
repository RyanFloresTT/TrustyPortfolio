using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Repositories {
    public interface IProjectRepository {
        Task<IEnumerable<Project>> GetAllAsync(string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1,
            int pageSize = 100);
        Task<Project?> GetByGuidAsync(Guid id);
        Task<Project?> GetByUrlHandleAsync(string urlHandle);
        Task<Project> AddAsync(Project project);
        Task<Project?> UpdateAsync(Project project);
        Task<Project?> DeleteAsync(Guid id);
        Task<IEnumerable<Project>> GetFeaturedAsync();
        Task<int> CountAsync();
    }
}

using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Repositories {
    public interface IProjectRepository {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByGuidAsync(Guid id);
        Task<Project?> GetByUrlHandleAsync(string urlHandle);
        Task<Project> AddAsync(Project project);
        Task<Project?> UpdateAsync(Project project);
        Task<Project?> DeleteAsync(Guid id);
    }
}

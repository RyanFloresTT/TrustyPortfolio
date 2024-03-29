using Microsoft.EntityFrameworkCore;
using TrustyPortfolio.Data;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Repositories {
    public class ProjectRepository (PortfolioDbContext db) : IProjectRepository {
        readonly PortfolioDbContext db =  db;
        public async Task<Project> AddAsync(Project project) {
            await db.AddAsync(project);
            await db.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> DeleteAsync(Guid id) {
            var project = await db.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (project != null) {
                db.Projects.Remove(project);
                await db.SaveChangesAsync();
                return project;
            }
            return null;
        }

        public async Task<IEnumerable<Project>> GetAllAsync(string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1,
            int pageSize = 100
            ) {

            var query = db.Projects.AsQueryable();

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

            return await query.Include(x => x.Tags).Include(x => x.Blogs).ToListAsync();
        }

        public async Task<Project?> GetByGuidAsync(Guid id) {
            return await db.Projects.Include(x => x.Tags)
                                    .Include(x => x.Blogs)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Project?> GetByUrlHandleAsync(string urlHandle) {
            return await db.Projects.Include(x => x.Tags)
                                    .Include(x => x.Blogs)
                                    .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<IEnumerable<Project>> GetFeaturedAsync() {
            return await db.Projects.Include(x => x.Tags).Where(x => x.Featured).ToListAsync();
        }
        public async Task<int> CountAsync() {
            return await db.Tags.CountAsync();
        }

        public async Task<Project?> UpdateAsync(Project project) {
            var existingProject = await db.Projects.Include(x => x.Tags)
                                                    .Include(x => x.Blogs)
                                                    .FirstOrDefaultAsync(x => x.Id == project.Id);

            if (existingProject != null) {
                existingProject.Id = project.Id;
                existingProject.Title = project.Title;
                existingProject.Heading = project.Heading;
                existingProject.Content = project.Content;
                existingProject.FeaturedImageUrl = project.FeaturedImageUrl;
                existingProject.UrlHandle = project.UrlHandle;
                existingProject.ProjectUrl = project.ProjectUrl;
                existingProject.Description = project.Description;
                existingProject.PublishDate = project.PublishDate;
                existingProject.Visible = project.Visible;
                existingProject.Featured = project.Featured;
                existingProject.Tags = project.Tags;
                existingProject.Blogs = project.Blogs;

                await db.SaveChangesAsync();
                return existingProject;
            }

            return null;
        }
    }
}

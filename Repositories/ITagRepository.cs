﻿using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Repositories {
    public interface ITagRepository {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByGuidAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}

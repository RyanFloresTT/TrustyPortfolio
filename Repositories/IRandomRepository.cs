namespace TrustyPortfolio.Repositories {
    public interface IRandomRepository<T> {
        Task<T> GetRandomAsync();
    }
}

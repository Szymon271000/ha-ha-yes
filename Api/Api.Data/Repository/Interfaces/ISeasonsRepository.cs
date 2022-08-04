namespace Api.Data.Repository.Interfaces
{
    public interface ISeasonsRepository
    {
        Task<Season?> CreateAsync(Season entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Season>> RetrieveAllAsync();
        Task<Season?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Season?> UpdateAsync(Season entity);
    }
}
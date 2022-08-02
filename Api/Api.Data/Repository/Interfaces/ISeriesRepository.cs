namespace Api.Data.Repository.Interfaces
{
    public interface ISeriesRepository
    {
        Task<Serie?> CreateAsync(Serie entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Serie>> RetrieveAllAsync();
        Task<Serie?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Serie?> UpdateAsync(Serie entity);
    }
}
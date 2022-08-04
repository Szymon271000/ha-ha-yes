namespace Api.Data.Repository.Interfaces
{
    public interface IGenresRepository
    {
        Task<Genre?> CreateAsync(Genre entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Genre>> RetrieveAllAsync();
        Task<Genre?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Genre?> UpdateAsync(Genre entity);
    }
}
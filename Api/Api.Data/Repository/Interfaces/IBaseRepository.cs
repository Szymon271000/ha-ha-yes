namespace Api.Data.Repository.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> CreateAsync(T entity);
    Task<IEnumerable<T>> RetrieveAllAsync();
    Task<T?> RetrieveAsync(int id);
    Task<T?> UpdateAsync(T entity);
    Task<bool?> DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}

namespace Api.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<T?> CreateAsync(T entity);
    Task<IEnumerable<T>> RetrieveAllAsync();
    Task<T?> RetrieveAsync(int id);
    Task<T?> UpdateAsync(int id, T entity);
    Task<bool?> DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}

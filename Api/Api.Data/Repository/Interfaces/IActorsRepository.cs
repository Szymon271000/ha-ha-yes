namespace Api.Data.Repository.Interfaces
{
    public interface IActorsRepository
    {
        Task<Actor?> CreateAsync(Actor entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Actor>> RetrieveAllAsync();
        Task<Actor?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Actor?> UpdateAsync(Actor entity);
    }
}
namespace Api.Data.Repository.Interfaces
{
    public interface IUsersRepository
    {
        Task<User?> CreateAsync(User entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<User>> RetrieveAllAsync();
        Task<User?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<User?> UpdateAsync(int id, User entity);
    }
}
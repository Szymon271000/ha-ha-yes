using Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Data.Repository;

public class UsersRepository : IBaseRepository<User>, IUsersRepository
{
    private ApiContext _context;
    public UsersRepository(ApiContext injectedContext)
    {
        _context = injectedContext;
    }
    public async Task<User?> CreateAsync(User entity)
    {
        EntityEntry<User> addedUser = await _context.Users.AddAsync(entity);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return entity;
        return null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        User? soughtUser = await _context.Users.FindAsync(id);
        if (soughtUser == null) return null;
        _context.Users.Remove(soughtUser);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return true;
        return null;
    }

    public async Task<IEnumerable<User>> RetrieveAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> RetrieveAsync(int id)
    {
        return await _context.Users.Include(u => u.Credentials).FirstOrDefaultAsync(u=>u.UserId==id);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<User?> UpdateAsync(int id, User entity)
    {
        _context.Users.Update(entity);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return entity;
        return null;
    }
}

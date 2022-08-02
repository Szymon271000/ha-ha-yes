using Api.Data.Model;
using Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Data.Repository;

public class SeasonsRepository : IBaseRepository<Season>, ISeasonsRepository
{
    private ApiContext _context;
    public SeasonsRepository(ApiContext injectedContext)
    {
        _context = injectedContext;
    }

    public async Task<Season?> CreateAsync(Season entity)
    {
        EntityEntry<Season> addedSeason = await _context.Seasons.AddAsync(entity);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return entity;
        return null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Season? soughtSeason = await _context.Seasons.FindAsync(id);
        if (soughtSeason == null) return null;
        _context.Seasons.Remove(soughtSeason);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return true;
        return null;
    }

    public async Task<IEnumerable<Season>> RetrieveAllAsync()
    {
        return await _context.Seasons.ToListAsync();
    }

    public async Task<Season?> RetrieveAsync(int id)
    {
        return await _context.Seasons.FindAsync(id);
    }

    public async Task<Season?> UpdateAsync(Season entity)
    {
        _context.Seasons.Update(entity);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return entity;
        return null;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

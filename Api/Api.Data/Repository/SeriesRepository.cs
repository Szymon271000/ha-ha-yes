﻿using Api.Data.Model;
using Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Data.Repository;

public class SeriesRepository : IBaseRepository<Serie>, ISeriesRepository
{
    private ApiContext _context;
    public SeriesRepository(ApiContext injectedContext)
    {
        _context = injectedContext;
    }

    public async Task<Serie?> CreateAsync(Serie entity)
    {
        EntityEntry<Serie> addedSerie = await _context.Series.AddAsync(entity);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return entity;
        return null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Serie? soughtSerie = await _context.Series.FindAsync(id);
        if (soughtSerie == null) return null;
        _context.Series.Remove(soughtSerie);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return true;
        return null;
    }

    public async Task<IEnumerable<Serie>> RetrieveAllAsync()
    {
        return await _context.Series.ToListAsync();
    }

    public async Task<Serie?> RetrieveAsync(int id)
    {
        return await _context.Series.FindAsync(id);
    }

    public async Task<Serie?> UpdateAsync(Serie entity)
    {
        _context.Series.Update(entity);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return entity;
        return null;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}


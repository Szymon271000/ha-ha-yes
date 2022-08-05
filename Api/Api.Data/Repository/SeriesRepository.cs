using Api.Data.Model.PaginationModel;
namespace Api.Data.Repository;

public class SeriesRepository : ISeriesRepository
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

    public async Task<Serie?> RetrieveWithSeasonsAndEpisodesAsync(int id)
    => await _context.Series
        .Include(x => x.SerieSeasons)
        .ThenInclude(x => x.SeasonEpisodes)
        .Where(x => x.SerieId == id)
        .FirstOrDefaultAsync();

    public async Task<Serie?> RetrieveWithSeasonsAndEpisodesAndActorsAsync(int id)
    => await _context.Series
        .Include(x => x.SerieSeasons)
        .ThenInclude(x => x.SeasonEpisodes)
        .ThenInclude(x => x.EpisodeActors)
        .Where(x => x.SerieId == id)
        .FirstOrDefaultAsync();


    public async Task<Serie?> UpdateAsync(int id, Serie entity)
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

    public async Task<Serie?> RetrieveSerieWithGenresAsync(int id)
    {
        return await _context.Series.Include(x => x.SerieGenres).Where(x => x.SerieId == id).FirstOrDefaultAsync();
    }

    public async Task<Serie?> RetrieveSerieWithSeasonsAsync(int id)
    {
        return await _context.Series.Include(x => x.SerieSeasons).Where(x => x.SerieId == id).FirstOrDefaultAsync();
    }

    public async Task<Serie?> UpdateAsync(Serie entity)
    {
        _context.Series.Update(entity);
        int affectedRows = await SaveChangesAsync();
        if (affectedRows == 1) return entity;
        return null;
    }

    public async Task<IEnumerable<Serie>> GetSeries(SerieParameter serieParameters)
    {
        var result = await _context.Series
            .OrderBy(series => series.SerieName)
            .Skip((serieParameters.PageNumber - 1) * serieParameters.PageSize)
            .Take(serieParameters.PageSize)
            .ToListAsync();

        return result;
    }
}


using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Data.Repository
{
    public class GenresRepository : IBaseRepository<Genre>
    {
        private ApiContext _context;
        public GenresRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }
        public async Task<Genre?> CreateAsync(Genre entity)
        {
            EntityEntry<Genre> addedEpisode = await _context.Genres.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Genre? soughtGenre = await _context.Genres.FindAsync(id);
            if (soughtGenre == null) return null;
            _context.Genres.Remove(soughtGenre);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<Genre>> RetrieveAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> RetrieveAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Genre?> UpdateAsync(int id, Genre entity)
        {
            _context.Genres.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }
    }
}

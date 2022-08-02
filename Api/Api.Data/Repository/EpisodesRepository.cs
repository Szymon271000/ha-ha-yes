using Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class EpisodesRepository : IBaseRepository<Episode>, IEpisodesRepository
    {
        private ApiContext _context;
        public EpisodesRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }
        public async Task<Episode?> CreateAsync(Episode entity)
        {
            EntityEntry<Episode> addedEpisode = await _context.Episodes.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Season? soughtEpisode = await _context.Seasons.FindAsync(id);
            if (soughtEpisode == null) return null;
            _context.Seasons.Remove(soughtEpisode);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<Episode>> RetrieveAllAsync()
        {
            return await _context.Episodes.ToListAsync();
        }

        public async Task<Episode?> RetrieveAsync(int id)
        {
            return await _context.Episodes.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Episode?> UpdateAsync(int id, Episode entity)
        {
            _context.Episodes.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }
    }
}

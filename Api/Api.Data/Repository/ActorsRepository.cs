using Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class ActorsRepository : IBaseRepository<Actor>, IActorsRepository
    {
        private ApiContext _context;
        public ActorsRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }
        public async Task<Actor?> CreateAsync(Actor entity)
        {
            EntityEntry<Actor> addedActor = await _context.Actors.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Actor? soughtActor = await _context.Actors.FindAsync(id);
            if (soughtActor == null) return null;
            _context.Actors.Remove(soughtActor);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<Actor>> RetrieveAllAsync()
        {
            return await _context.Actors.ToListAsync();
        }

        public async Task<Actor?> RetrieveAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Actor?> UpdateAsync(Actor entity)
        {
            _context.Actors.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }
    }
}

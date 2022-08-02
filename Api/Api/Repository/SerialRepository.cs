using Api.Data.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Repository
{
    public class SerialRepository : IBaseRepository<Serial>
    {
        private ApiContext _context;
        public SerialRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }

        public async Task<Serial?> CreateAsync(Serial entity)
        {
            EntityEntry<Serial> addedSerial = await _context.Serials.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Serial? soughtSerial = await _context.Serials.FindAsync(id);
            if (soughtSerial == null) return null;
            _context.Serials.Remove(soughtSerial);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<Serial>> RetrieveAllAsync()
        {
            return await _context.Serials.ToListAsync();
        }

        public async Task<Serial?> RetrieveAsync(int id)
        {
            return await _context.Serials.FindAsync(id);
        }

        public async Task<Serial?> UpdateAsync(int id, Serial entity)
        {
            _context.Serials.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

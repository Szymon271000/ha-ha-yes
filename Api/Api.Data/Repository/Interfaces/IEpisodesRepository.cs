namespace Api.Data.Repository.Interfaces
{
    public interface IEpisodesRepository
    {
        Task<Episode?> CreateAsync(Episode entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Episode>> RetrieveAllAsync();
        Task<Episode?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Episode?> UpdateAsync(int id, Episode entity);
    }
}
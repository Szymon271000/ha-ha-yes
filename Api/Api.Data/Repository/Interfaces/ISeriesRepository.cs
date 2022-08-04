namespace Api.Data.Repository.Interfaces
{
    public interface ISeriesRepository : IBaseRepository<Serie>
    {
        Task<Serie?> RetrieveWithSeasonsAndEpisodesAsync(int id);

        Task<Serie?> RetrieveSerieWithGenresAsync(int id);

        Task<Serie?> RetrieveSerieWithSeasonsAsync(int id);
        Task<Serie?> CreateAsync(Serie entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Serie>> RetrieveAllAsync();
        Task<Serie?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Serie?> UpdateAsync(int id, Serie entity);
    }
}
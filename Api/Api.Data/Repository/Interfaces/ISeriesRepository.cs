namespace Api.Data.Repository.Interfaces
{
    public interface ISeriesRepository : IBaseRepository<Serie>
    {
        Task<Serie?> RetrieveWithSeasonsAndEpisodesAsync(int id);

        Task<Serie?> RetrieveSerieWithGenresAsync(int id);
    }
}
using Api.Data.Seeder.EntitiesSeeders;

namespace Api.Data.Seeder
{
    public static class DataSeeder
    {
        public static void SeedDB(this ModelBuilder builder)
        {
            builder.SeedSeries();
            builder.SeedSeasons();
            builder.SeedActors();
            builder.SeedEpisodes();
            builder.SeedActorEpisode();            
            builder.SeedGenres();
            builder.SeedGenreSerie();
        }

        
    }
}

namespace Api.Data.Seeder.EntitiesSeeders
{
    public static class GenresSeeder
    {
        public static void SeedGenres(this ModelBuilder builder)
        {
            List<Genre> genres = new();
            string[] fileLines = File.ReadAllLines("../Api.Data/Seeder/EntitiesSeeders/Data/Genres.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                genres.Add(item);
            }

            builder.Entity<Genre>().HasData(genres);
        }
        private static Genre GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string name = item[1];
            return new Genre() { GenreId = id, GenreName = name };
        }
    }
}

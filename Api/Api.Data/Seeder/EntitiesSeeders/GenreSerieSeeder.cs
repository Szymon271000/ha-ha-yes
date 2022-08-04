namespace Api.Data.Seeder.EntitiesSeeders
{
    public static class GenreSerieSeeder
    {
        public static void SeedGenreSerie(this ModelBuilder builder)
        {
            List<GenreSerie> relations = new();
            string[] fileLines = File.ReadAllLines("../Api.Data/Seeder/EntitiesSeeders/Data/GenreSerie.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                relations.Add(item);
            }

            builder.Entity<GenreSerie>().HasData(relations);
        }
        private static GenreSerie GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int genreId = Convert.ToInt32(item[0]);
            int serieId = Convert.ToInt32(item[1]);

            return new GenreSerie() { GenreId = genreId, SerieId = serieId };
        }
    }
}

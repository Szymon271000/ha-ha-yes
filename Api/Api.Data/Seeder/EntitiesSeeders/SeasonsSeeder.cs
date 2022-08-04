namespace Api.Data.Seeder.EntitiesSeeders
{
    public static class SeasonsSeeder
    {
        public static void SeedSeasons(this ModelBuilder builder)
        {
            List<Season> seasons = new();
            string[] fileLines = File.ReadAllLines("../Api.Data/Seeder/EntitiesSeeders/Data/Seasons.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                seasons.Add(item);
            }

            builder.Entity<Season>().HasData(seasons);
        }
        private static Season GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            int seasonNumber = Convert.ToInt32(item[1]);
            int serieId = Convert.ToInt32(item[2]);
            return new Season() { SeasonId = id, SeasonNumber = seasonNumber, SerieId = serieId };
        }
    }
}

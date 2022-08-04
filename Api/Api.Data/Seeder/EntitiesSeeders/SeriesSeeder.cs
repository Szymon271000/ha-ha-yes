namespace Api.Data.Seeder.EntitiesSeeders
{
    public static class SeriesSeeder
    {
        public static void SeedSeries(this ModelBuilder builder)
        {
            List<Serie> series = new();
            string[] fileLines = File.ReadAllLines("../Api.Data/Seeder/EntitiesSeeders/Data/Series.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                series.Add(item);
            }

            builder.Entity<Serie>().HasData(series);
        }
        private static Serie GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string name = item[1];
            return new Serie() { SerieId = id, SerieName = name };
        }
    }
}

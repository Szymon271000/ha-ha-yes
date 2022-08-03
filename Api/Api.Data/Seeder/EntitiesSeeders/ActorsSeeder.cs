namespace Api.Data.Seeder.EntitiesSeeders
{
    public static class ActorsSeeder
    {
        public static void SeedActors(this ModelBuilder builder)
        {
            List<Actor> actors = new List<Actor>();
            string[] fileLines = File.ReadAllLines("../Api.Data/Seeder/EntitiesSeeders/Data/Actors.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                actors.Add(item);
            }

            builder.Entity<Actor>().HasData(actors);
        }

        private static Actor GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string name = item[1];
            return new Actor() { ActorId = id, ActorName = name };
        }
    }
}

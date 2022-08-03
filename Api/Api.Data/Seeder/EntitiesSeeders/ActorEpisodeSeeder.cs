using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Seeder.EntitiesSeeders
{
    public static class ActorEpisodeSeeder
    {
        public static void SeedActorEpisode(this ModelBuilder builder)
        {
            List<ActorEpisode> relations = new();
            string[] fileLines = File.ReadAllLines("../Api.Data/Seeder/EntitiesSeeders/Data/ActorEpisode.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                relations.Add(item);
            }

            builder.Entity<ActorEpisode>().HasData(relations);
        }
        private static ActorEpisode GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int actorId = Convert.ToInt32(item[0]);
            int episodeId = Convert.ToInt32(item[1]);

            return new ActorEpisode() { ActorId = actorId, EpisodeId = episodeId }; 
        }
    }
}

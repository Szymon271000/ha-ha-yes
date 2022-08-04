using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Seeder.EntitiesSeeders
{
    public static class EpisodesSeeder
    {
        public static void SeedEpisodes(this ModelBuilder builder)
        {
            List<Episode> episodes = new();
            string[] fileLines = File.ReadAllLines("../Api.Data/Seeder/EntitiesSeeders/Data/Episodes.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                episodes.Add(item);
            }

            builder.Entity<Episode>().HasData(episodes);
        }
        private static Episode GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string name = item[1];
            int number = Convert.ToInt32(item[2]);
            int seasonId = Convert.ToInt32(item[3]);
            return new Episode() { EpisodeId = id, EpisodeName = name, EpisodeNumber = number, SeasonId = seasonId };
        }
    }
}

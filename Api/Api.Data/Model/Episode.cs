namespace Api.Data.Model
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        public int? EpisodeNumber { get; set; }
        public string? EpisodeName { get; set; }
        public Season? EpisodeSeason { get; set; }
        public int? SeasonId { get; set; }
        public List<Actor>? EpisodeActors { get; set; }
    }
}

namespace Api.Data.Model
{
    public class ActorEpisode
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
    }
}

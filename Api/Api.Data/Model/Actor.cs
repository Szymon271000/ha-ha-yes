namespace Api.Data.Model
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        public string? ActorName { get; set; }
        public List<Episode>? ActorEpisodes { get; set; } = new List<Episode>();
    }
}

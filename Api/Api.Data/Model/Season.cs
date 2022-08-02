namespace Api.Data.Model
{
    public class Season
    {
        [Key]
        public int SeasonId { get; set; }
        public int? SeasonNumber { get; set; }
        public Serie? SeasonSerie { get; set; }
        public int? SerieId { get; set; }
        public List<Episode>? SeasonEpisodes { get; set; } = new List<Episode>();
    }
}

namespace Api.Data.Model
{
    public class Season
    {
        [Key]
        public int SeasonId { get; set; }
        public int? SeasonNumber { get; set; }
        public Serial? SeasonSerial { get; set; }
        public int? SerialId { get; set; }
        public List<Episode>? SeasonEpisodes { get; set; }
    }
}

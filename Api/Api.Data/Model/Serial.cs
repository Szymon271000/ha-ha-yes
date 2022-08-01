namespace Api.Data.Model
{
    public class Serial
    {
        public int SerialId { get; set; }
        public string? SerialName { get; set; }
        public List<Season>? SerialSeasons { get; set; }
        public List<Genre>? SerialGenres { get; set; }
    }
}

namespace Api.Data.Model
{
    public class Serie
    {
        [Key]
        public int SerieId { get; set; }
        public string? SerieName { get; set; }
        public List<Season>? SerieSeasons { get; set; }
        public List<Genre>? SerieGenres { get; set; }
    }
}

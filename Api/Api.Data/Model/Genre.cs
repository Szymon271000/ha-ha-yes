namespace Api.Data.Model
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public List<Serie>? GenreSerie { get; set; } = new List<Serie>();
    }
}

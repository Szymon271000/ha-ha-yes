namespace Api.Data.Model
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public List<Serial>? GenreSerials { get; set; }
    }
}

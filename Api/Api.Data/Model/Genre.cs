namespace Api.Data.Model
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public List<Serial>? GenreSerials { get; set; }
    }
}

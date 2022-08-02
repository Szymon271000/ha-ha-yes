namespace Api.DTOs.GenresDTOs
{
    public class GenreUpdateDto
    {
        [Required]
        public string? GenreName { get; set; }
    }
}

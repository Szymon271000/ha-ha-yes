namespace Api.DTOs.GenresDTOs
{
    public class GenreUpdateDto
    {
        [Required]
        [MaxLengthAttribute(50)]
        public string GenreName { get; set; }
    }
}

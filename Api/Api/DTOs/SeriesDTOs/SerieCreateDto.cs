namespace Api.DTOs.SeriesDTOs
{
    public class SerieCreateDto
    {
        [Required]
        [MaxLengthAttribute(50)]
        public string SerieName { get; set; }
    }
}

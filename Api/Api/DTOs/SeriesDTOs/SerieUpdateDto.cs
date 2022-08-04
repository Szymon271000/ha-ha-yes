namespace Api.DTOs.SeriesDTOs
{
    public class SerieUpdateDto
    {
        [Required]
        [MaxLengthAttribute(50)]
        public string SerieName { get; set; }

    }
}

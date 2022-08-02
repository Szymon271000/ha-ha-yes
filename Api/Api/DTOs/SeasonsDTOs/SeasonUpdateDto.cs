namespace Api.DTOs.SeasonsDTOs
{
    public class SeasonUpdateDto
    {
        [Required]
        public int? SeasonNumber { get; set; }
    }
}

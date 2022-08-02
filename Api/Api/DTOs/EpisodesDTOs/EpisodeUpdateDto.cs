namespace Api.DTOs.EpisodesDTOs
{
    public class EpisodeUpdateDto
    {
        [Required]
        public int? EpisodeNumber { get; set; }
        [Required]
        public string? EpisodeName { get; set; }
    }
}

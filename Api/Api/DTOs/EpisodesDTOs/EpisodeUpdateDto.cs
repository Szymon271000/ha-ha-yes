namespace Api.DTOs.EpisodesDTOs
{
    public class EpisodeUpdateDto
    {
        [Required]
        public int EpisodeNumber { get; set; }
        [Required]
        [MaxLengthAttribute(50)]
        public string EpisodeName { get; set; }
    }
}

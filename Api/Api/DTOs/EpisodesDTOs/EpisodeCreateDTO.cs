namespace Api.DTOs.EpisodesDTOs;

public class EpisodeCreateDTO
{
    [Required]
    public int EpisodeNumber { get; set; }
    [Required]
    [MaxLengthAttribute(50)]
    public string EpisodeName { get; set; }
}

namespace Api.DTOs.ActorsDTOs
{
    public class ActorUpdateDto
    {
        [Required]
        [MaxLengthAttribute(50)]
        public string ActorName { get; set; }

    }
}

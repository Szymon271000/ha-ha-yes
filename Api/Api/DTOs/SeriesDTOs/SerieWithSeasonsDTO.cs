namespace Api.DTOs.SeriesDTOs
{
    public class SerieWithSeasonsDTO
    {
        public int? SerieId { get; set; }
        public string? SerieName { get; set; }

        public IEnumerable<SimpleSeasonDTO> Seasons { get; set; }
    }
}

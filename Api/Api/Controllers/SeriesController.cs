namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IBaseRepository<Genre> _genresRepository;
        private readonly IMapper _mapper;

        public SeriesController(ISeriesRepository seriesRepository, IBaseRepository<Genre> genresRepository,  IMapper mapper)
        {
            _seriesRepository = seriesRepository;
            _genresRepository = genresRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all series with Name and Ids
        /// </summary>
        /// <returns>All series in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "SerieId": "",
        ///        "SerieName": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns all series</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllSeries()
        {
            var series = await _seriesRepository.RetrieveAllAsync();
            return Ok(_mapper.Map<IEnumerable<SimpleSerieDTO>>(series));
        }


        /// <summary>
        /// Get serie by ID with Name and Ids
        /// </summary>
        /// <returns>Serie in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "SerieId": "",
        ///        "SerieName": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns serie with specific ID</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSerialById(int id)
        {
            var serial = await _seriesRepository.RetrieveAsync(id);
            if (serial == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SimpleSerieDTO>(serial));
        }

        /// <summary>
        /// Add genre to serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns>Updated list of genres in specific serie </returns>
        /// <response code="204">The genre has been added to serie</response>
        /// <response code="400">If the serie is null</response>
        /// <response code="400">If the genre is null</response>
        [HttpPut("id/genres/{genreId}")]
        public async Task<IActionResult> AddGenreToSerial(int id, int genreId)
        {
            var serial = await _seriesRepository.RetrieveAsync(id);
            if (serial == null)
            {
                return NotFound();
            }
            var genre = await _genresRepository.RetrieveAsync(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            serial.SerieGenres.Add(genre);
            genre.GenreSerie.Add(serial);
            await _seriesRepository.UpdateAsync(serial.SerieId, serial);
            await _genresRepository.UpdateAsync(genre.GenreId, genre);
            await _genresRepository.SaveChangesAsync();
            await _seriesRepository.SaveChangesAsync();
            return NoContent();
        }



        [HttpGet("{id}/seasons/{seasonNumber}/episodes")]
        public async Task<ActionResult<List<SimpleEpisodeDTO>>> GetEpisodesOfSeason(int id, int seasonNumber)
        {
            Serie serie = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAsync(id);
            var season = serie.SerieSeasons.Where(x => x.SeasonNumber == seasonNumber).FirstOrDefault();
            var episodes = season.SeasonEpisodes;
            List<SimpleEpisodeDTO> result = new List<SimpleEpisodeDTO>();
            episodes.ForEach(x => result.Add(_mapper.Map<SimpleEpisodeDTO>(x)));

            return Ok(result);
        }
    }
}

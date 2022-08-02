


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IBaseRepository<Serie> _seriesRepository;
        private readonly IBaseRepository<Genre> _genresRepository;
        private readonly IMapper _mapper;

        public SeriesController(IBaseRepository<Serie> seriesRepository, IBaseRepository<Genre> genresRepository, IMapper mapper)
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
    }
}

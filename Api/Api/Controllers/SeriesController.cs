


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IBaseRepository<Serie> _seriesRepository;
        private readonly IMapper _mapper;

        public SeriesController(IBaseRepository<Serie> seriesRepository, IMapper mapper)
        {
            _seriesRepository = seriesRepository;
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
    }
}

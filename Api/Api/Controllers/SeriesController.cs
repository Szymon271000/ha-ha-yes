namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IMapper _mapper;

        public SeriesController(ISeriesRepository seriesRepository, IMapper mapper)
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
        /// <response code="200">Returns all series</response>
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

        //Patch api/series/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<SerieUpdateDto> patchDoc)
        {
            var modelFromRepo = await _seriesRepository.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<SerieUpdateDto>(modelFromRepo);
            patchDoc.ApplyTo(entityToPatch, ModelState);
            if (!TryValidateModel(entityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(entityToPatch, modelFromRepo);
            await _seriesRepository.UpdateAsync(modelFromRepo);
            //await _repository.SaveChanges();
            return NoContent();
        }
    }
}

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeasonsController : ControllerBase
{
    private readonly ISeasonsRepository _repository;
    private readonly IMapper _mapper;
    public SeasonsController(ISeasonsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Add new season
    /// </summary>
    /// <returns>Add new season</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "seasonNumber": 23
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Created</response>
    /// <response code="200">OK</response>
    /// <response code="400">Bad request</response>

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create(SeasonCreateDTO newSeason)
    {
        var createdSeason = await _repository.CreateAsync(_mapper.Map<Season>(newSeason));
        if (createdSeason == null) return BadRequest();

        var entity = await _repository.RetrieveAsync(createdSeason.SeasonId);
        var readDto = _mapper.Map<SeasonGetDTO>(entity);
        return CreatedAtRoute(nameof(GetSeason), new { Id = readDto.SeasonId }, readDto);
    }

    /// <summary>
    /// Delete season
    /// </summary>
    /// <returns>Delete season</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     23
    ///
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="404">Not Found</response>

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await _repository.DeleteAsync(id);
        if (result == null) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Get season by id
    /// </summary>
    /// <returns>Get season by id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     23
    ///
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="404">Not Found</response>

    [HttpGet]
    [Route("{id}", Name = "GetSeason")]
    public async Task<IActionResult> GetSeason(int id)
    {
        var soughtSeason = await _repository.RetrieveAsync(id);
        if (soughtSeason == null) return NotFound();
        return Ok(_mapper.Map<SeasonGetDTO>(soughtSeason));
    }

    /// <summary>
    /// Get all genres
    /// </summary>
    /// <returns>Get all seasons</returns>
    /// <response code="200">OK</response>

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll()
    {
        var fetchedSeasons = await _repository.RetrieveAllAsync();
        return Ok(_mapper.Map<IList<SeasonGetDTO>>(fetchedSeasons));
    }

    /// <summary>
    /// Update season number
    /// </summary>
    /// <returns>Update season name</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///        "op": "replace",
    ///        "path": "SeasonNumber",
    ///        "value": "5"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">No content</response>
    /// <response code="200">OK</response>
    /// <response code="400">If the item is null</response>

    //Patch api/seasons/{id}
    [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<SeasonUpdateDto> patchDoc)
        {
            var modelFromRepo = await _repository.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<SeasonUpdateDto>(modelFromRepo);
            patchDoc.ApplyTo(entityToPatch, ModelState);
            if (!TryValidateModel(entityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(entityToPatch, modelFromRepo);
            await _repository.UpdateAsync(modelFromRepo);
            //await _repository.SaveChanges();
            return NoContent();
        }
}

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

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Create(SeasonCreateDTO newSeason)
    {
        var createdSeason = await _repository.CreateAsync(_mapper.Map<Season>(newSeason));
        if (createdSeason == null) return BadRequest();
        return Ok();
    }

    [HttpDelete]
    [Route("remove")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await _repository.DeleteAsync(id);
        if (result == null) return NotFound();
        return NoContent();
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var soughtSeason = await _repository.RetrieveAsync(id);
        if (soughtSeason == null) return NotFound();
        return Ok(_mapper.Map<SeasonGetDTO>(soughtSeason));
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetAll()
    {
        var fetchedSeasons = await _repository.RetrieveAllAsync();
        return Ok(_mapper.Map<IList<SeasonGetDTO>>(fetchedSeasons));
    }

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

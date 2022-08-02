namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodesRepository _repository;
        private readonly IMapper _mapper;
        public EpisodesController(IEpisodesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Patch api/episodes/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<EpisodeUpdateDto> patchDoc)
        {
            var modelFromRepo = await _repository.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<EpisodeUpdateDto>(modelFromRepo);
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
}

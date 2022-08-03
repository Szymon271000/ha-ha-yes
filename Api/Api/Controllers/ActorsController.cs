

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorsRepository _repository;
        private readonly IMapper _mapper;
        public ActorsController(IActorsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(string name)
        {
            var createdGenre = await _repository.CreateAsync(new Actor() { ActorName = name });
            if (createdGenre == null) return BadRequest();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var soughtActor = await _repository.RetrieveAsync(id);
            if (soughtActor == null) return NotFound();
            return Ok(_mapper.Map<ActorGetDTO>(soughtActor));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var fetchedActors = await _repository.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<ActorGetDTO>>(fetchedActors));
        }


        /// <summary>
        /// Update actor name
        /// </summary>
        /// <returns>Update actor name</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "op": "replace",
        ///        "path": "ActorName",
        ///        "value": "NewName"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>

        //Patch api/actors/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<ActorUpdateDto> patchDoc)
        {
            var modelFromRepo = await _repository.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<ActorUpdateDto>(modelFromRepo);
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

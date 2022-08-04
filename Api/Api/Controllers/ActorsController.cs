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

        /// <summary>
        /// Add new actor
        /// </summary>
        /// <returns>Add new actor</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Robert DeNiro
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(string name)
        {
            var createdGenre = await _repository.CreateAsync(new Actor() { ActorName = name });
            if (createdGenre == null) return BadRequest();

            var entity = await _repository.RetrieveAsync(createdGenre.ActorId);
            var readDto = _mapper.Map<ActorGetDTO>(entity);
            return CreatedAtRoute(nameof(GetActor), new { Id = readDto.ActorId }, readDto);
        }

        /// <summary>
        /// Delete actor
        /// </summary>
        /// <returns>Delete actor</returns>
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
        /// Get actor by id
        /// </summary>
        /// <returns>Get actor by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     23
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>

        [HttpGet]
        [Route("{id}", Name = "GetActor")]
        public async Task<IActionResult> GetActor(int id)
        {
            var soughtActor = await _repository.RetrieveAsync(id);
            if (soughtActor == null) return NotFound();
            return Ok(_mapper.Map<ActorGetDTO>(soughtActor));
        }

        /// <summary>
        /// Get all actors
        /// </summary>
        /// <returns>Get all actors</returns>
        /// <response code="200">OK</response>

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

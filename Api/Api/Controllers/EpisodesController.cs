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

        /// <summary>
        /// Add new episode
        /// </summary>
        /// <returns>Add new episode</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "episodeNumber": 0,
        ///         "episodeName": "Name of episode"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>

    [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(EpisodeCreateDTO newEpisode)
        {
            var createdGenre = await _repository.CreateAsync(_mapper.Map<Episode>(newEpisode));
            if (createdGenre == null) return BadRequest();

            var entity = await _repository.RetrieveAsync(createdGenre.EpisodeId);
            var readDto = _mapper.Map<EpisodeGetDTO>(entity);
            return CreatedAtRoute(nameof(GetEpisode), new { Id = readDto.EpisodeId }, readDto);
        }

        /// <summary>
        /// Delete episode
        /// </summary>
        /// <returns>Delete episode</returns>
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
        /// Get episode by id
        /// </summary>
        /// <returns>Get episode by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     23
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>

        [HttpGet]
        [Route("{id}", Name = "GetEpisode")]
        public async Task<IActionResult> GetEpisode(int id)
        {
            var soughtEpisode = await _repository.RetrieveAsync(id);
            if (soughtEpisode == null) return NotFound();
            return Ok(_mapper.Map<EpisodeGetDTO>(soughtEpisode));
        }

        /// <summary>
        /// Get all episodes
        /// </summary>
        /// <returns>Get all episodes</returns>
        /// <response code="200">OK</response>

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var fetchedEpisodes = await _repository.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<EpisodeGetDTO>>(fetchedEpisodes));
        }



        /// <summary>
        /// Update episode name or number
        /// </summary>
        /// <returns>Update episode name</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "op": "replace",
        ///        "path": "EpisodeName",
        ///        "value": "NewName"
        ///     }
        ///
        ///     Or
        ///
        ///     {
        ///        "op": "replace",
        ///        "path": "EpisodeNumber",
        ///        "value": "5"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>

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

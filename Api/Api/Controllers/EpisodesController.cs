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

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Create(EpisodeCreateDTO newEpisode)
        {
            var createdGenre = await _repository.CreateAsync(_mapper.Map<Episode>(newEpisode));
            if (createdGenre == null) return BadRequest();
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
            var soughtEpisode = await _repository.RetrieveAsync(id);
            if (soughtEpisode == null) return NotFound();
            return Ok(_mapper.Map<EpisodeGetDTO>(soughtEpisode));
        }

        [HttpGet]
        [Route("get")]
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

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresRepository _repository;
        private readonly IMapper _mapper;
        public GenresController(IGenresRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new genre
        /// </summary>
        /// <returns>Add new genre</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Comedy
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [Route("", Name = "xyz")]
        public async Task<IActionResult> Create(string name)
        {
            var createdGenre = await _repository.CreateAsync(new Genre() { GenreName = name });
            if (createdGenre == null) return BadRequest();

            var entity = await _repository.RetrieveAsync(createdGenre.GenreId);
            var readDto = _mapper.Map<GenreGetDTO>(entity);
            return CreatedAtRoute(nameof(GetGenre), new { Id = readDto.GenreId }, readDto);
        }

        /// <summary>
        /// Delete genre
        /// </summary>
        /// <returns>Delete genre</returns>
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
        /// Get genre by id
        /// </summary>
        /// <returns>Get genre by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     23
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>

        [HttpGet]
        [Route("{id}", Name = "GetGenre")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var soughtGenre = await _repository.RetrieveAsync(id);
            if (soughtGenre == null) return NotFound();
            return Ok(_mapper.Map<GenreGetDTO>(soughtGenre));
        }

        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns>Get all genres</returns>
        /// <response code="200">OK</response>

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var fetchedGenres = await _repository.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<GenreGetDTO>>(fetchedGenres));
        }



        /// <summary>
        /// Update genre name
        /// </summary>
        /// <returns>Update genre name</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "op": "replace",
        ///        "path": "GenreName",
        ///        "value": "NewName"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>

        //Patch api/genres/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<GenreUpdateDto> patchDoc)
        {
            var modelFromRepo = await _repository.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<GenreUpdateDto>(modelFromRepo);
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

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

        [HttpPost]
        [Route("add", Name = "xyz")]
        public async Task<IActionResult> Create(string name)
        {
            var createdGenre = await _repository.CreateAsync(new Genre() { GenreName = name });
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
            var soughtGenre = await _repository.RetrieveAsync(id);
            if (soughtGenre == null) return NotFound();
            return Ok(_mapper.Map<GenreGetDTO>(soughtGenre));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAll()
        {
            var fetchedGenres = await _repository.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<GenreGetDTO>>(fetchedGenres));
        }

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

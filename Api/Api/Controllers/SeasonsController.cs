namespace Api.Controllers
{
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
}

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IGenresRepository _genresRepository;
        private readonly ISeasonsRepository _seasonRepository;
        private readonly IEpisodesRepository _episodesRepository;
        private readonly IMapper _mapper;

        public SeriesController(ISeriesRepository seriesRepository, IGenresRepository genresRepository, IEpisodesRepository episodesRepository, ISeasonsRepository seasonRepository, IMapper mapper)
        {
            _seriesRepository = seriesRepository;
            _genresRepository = genresRepository;
            _episodesRepository = episodesRepository;
            _seasonRepository = seasonRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all series with Name and Ids
        /// </summary>
        /// <returns>All series in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "SerieId": "",
        ///        "SerieName": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns all series</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllSeries()
        {
            var series = await _seriesRepository.RetrieveAllAsync();
            return Ok(_mapper.Map<IEnumerable<SimpleSerieDTO>>(series));
        }


        /// <summary>
        /// Get serie by ID with Name and Ids
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Serie in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "SerieId": "",
        ///        "SerieName": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns serie with specific ID</response>
        /// <response code="400">If the item is null</response>


        [HttpGet("{id}",Name = "GetSerieById")]
        public async Task<IActionResult> GetSerieById(int id)
        {
            var serie = await _seriesRepository.RetrieveAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SimpleSerieDTO>(serie));
        }

        /// <summary>
        /// Add genre to serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns>Updated list of genres in specific serie </returns>
        /// <response code="204">The genre has been added to serie</response>
        /// <response code="400">If the serie is null</response>
        /// <response code="400">If the genre is null</response>
        [HttpPut("{id}/genres/{genreId}")]
        public async Task<IActionResult> AddGenreToSerie(int id, int genreId)
        {
            var serie = await _seriesRepository.RetrieveAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            var genre = await _genresRepository.RetrieveAsync(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            serie.SerieGenres.Add(genre);
            await _seriesRepository.UpdateAsync(serie.SerieId, serie);
            await _seriesRepository.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Delete genre from serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns>Updated list of genres in specific serie </returns>
        /// <response code="204">The genre has been deleted to serie</response>
        /// <response code="400">If the serie is null</response>
        /// <response code="400">If the genre is null</response>
        [HttpDelete("{id}/genres/{genreId}")]
        public async Task<IActionResult> DeleteGenreFromSerie(int id, int genreId)
        {
            var serie = await _seriesRepository.RetrieveSerieWithGenresAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            var genre = serie.SerieGenres.Where(x => x.GenreId == genreId).FirstOrDefault();
            if (genre == null)
            {
                return NotFound();
            }
            serie.SerieGenres.Remove(genre);
            await _seriesRepository.UpdateAsync(serie.SerieId, serie);
            await _seriesRepository.SaveChangesAsync();
            return NoContent();
        }



        /// <summary>
        /// Get seasons of specific serie by id
        /// </summary>
        /// <returns>Seasons of specific serie in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "SerieId": "",
        ///        "SerieName": "",
        ///        "Seasons":
        ///        {
        ///         "seasonNumber":
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns seasons of specific serie</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}/seasons")]
        public async Task<IActionResult> GetSerieWithSeasonsById(int id)
        {
            var serie = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SerieWithSeasonsDTO>(serie));
        }


        /// <summary>
        /// Add season to serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonsId"></param>
        /// <returns>Updated list of seasons in specific serie </returns>
        /// <response code="204">The season has been added to serie</response>
        /// <response code="400">If the serie is null</response>
        /// <response code="400">If the season is null</response>
        [HttpPut("{id}/seasons/{seasonsId}")]
        public async Task<IActionResult> AddSeasonToSerie(int id, int seasonsId)
        {
            var serie = await _seriesRepository.RetrieveAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            var season = await _seasonRepository.RetrieveAsync(seasonsId);
            if (season == null)
            {
                return NotFound();
            }
            serie.SerieSeasons.Add(season);
            await _seriesRepository.UpdateAsync(serie.SerieId, serie);
            await _seriesRepository.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Delete season from serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <returns>Updated list of seasons in specific serie </returns>
        /// <response code="204">The season has been deleted from serie</response>
        /// <response code="400">If the serie is null</response>
        /// <response code="400">If the season is null</response>
        [HttpDelete("{id}/seasons/{seasonNumber}")]
        public async Task<IActionResult> DeleteSeasonFromSerie(int id, int seasonNumber)
        {
            var serie = await _seriesRepository.RetrieveSerieWithSeasonsAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            var season = serie.SerieSeasons.Where(x => x.SeasonNumber == seasonNumber).FirstOrDefault();
            if (season == null)
            {
                return NotFound();
            }

            serie.SerieSeasons.Remove(season);
            await _seriesRepository.UpdateAsync(serie.SerieId, serie);
            await _seriesRepository.SaveChangesAsync();
            return NoContent();
        }



        /// <summary>
        /// Get season of specific seasonNumber of a serie by identified by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <returns>Serie in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "seasonNumber": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns season with specific seasonNumber</response>
        /// <response code="400">If the serie is null</response>
        /// <response code="400">If the season is null</response>
        [HttpGet("{id}/season/{seasonNumber}")]
        public async Task<IActionResult> GetSeasonOfSpecificSerieById(int id, int seasonNumber)
        {
            var serie = await _seriesRepository.RetrieveSerieWithSeasonsAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            var season = serie.SerieSeasons.Where(x=> x.SeasonNumber == seasonNumber).FirstOrDefault();
            if (season == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SimpleSeasonDTO>(season));
        }


        /// <summary>
        /// Get episodes of target season of the serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <returns>List of episodes </returns>
        ///         /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "id": "1"
        ///        "seasonNumber": "1",
        ///     }
        ///
        /// <response code="200">When list of episodes was returned</response>
        /// <response code="404">If any object doesn't exist</response>
        [HttpGet("{id}/seasons/{seasonNumber}/episodes")]
        public async Task<ActionResult<List<SimpleEpisodeDTO>>> GetEpisodesOfSeason(int id, int seasonNumber)
        {
            Serie serie = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAsync(id);
            if(serie == null)
                return NotFound();

            var season = serie.SerieSeasons.Where(x => x.SeasonNumber == seasonNumber).FirstOrDefault();
            if (season == null)
                return NotFound();

            List<Episode> episodes = season.SeasonEpisodes;
            var episodesSorted = episodes.OrderBy(x => x.EpisodeNumber).ToList();

            return Ok(_mapper.Map<IEnumerable<SimpleEpisodeDTO>>(episodesSorted));
        }

        /// <summary>
        /// Add target episode to the season of the target serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <param name="episodeId"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">If episode was added</response>
        /// <response code="404">If any of the objects was not found</response>


        [HttpPut("{id}/seasons/{seasonNumber}/episodes/{episodeId}")]
        public async Task<ActionResult> AddEpisodeToSeason(int id, int seasonNumber, int episodeId)
        {
            var serie = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAsync(id);
            if (serie == null)
                return NotFound();

            var season = serie.SerieSeasons.Where(x => x.SeasonNumber == seasonNumber).FirstOrDefault();
            if (season == null)
                return NotFound();

            var episode = await _episodesRepository.RetrieveAsync(episodeId);
            if (episode == null)
                return NotFound();

            season.SeasonEpisodes.Add(episode);
            await _seriesRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete target episode from the season of the target serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <param name="episodeId"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">If episode was deleted</response>
        /// <response code="404">If any of the objects was not found</response>
        [HttpDelete("{id}/seasons/{seasonNumber}/episodes/{episodeId}")]
        public async Task<ActionResult> DeleteEpisodeFromSeason (int id, int seasonNumber, int episodeId)
        {
            var serie = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAsync(id);
            if (serie == null)
                return NotFound();

            var season = serie.SerieSeasons.Where(x => x.SeasonNumber == seasonNumber).FirstOrDefault();
            if (season == null)
                return NotFound();

            var episode = await _episodesRepository.RetrieveAsync(episodeId);
            if (episode == null || !season.SeasonEpisodes.Any(x => x.EpisodeId == episodeId))
                return NotFound();

            season.SeasonEpisodes.Remove(episode);
            await _seriesRepository.SaveChangesAsync();

            return NoContent();
        }






        /// <summary>
        /// Get target episode of target season of the serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <param name="episodeNumber"></param>
        /// <returns>Target episode </returns>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///         "id": "1",
        ///         "seasonNumber": "1",
        ///         "episodeNumber": "1"
        ///     }
        /// <response code="200">When target episode was returned</response>
        /// <response code="404">If any object doesn't exist</response>
        [HttpGet("{id}/seasons/{seasonNumber}/episodes/{episodeNumber}")]
        public async Task<ActionResult<SimpleEpisodeDTO>> GetEpisodeFromSerie(int id, int seasonNumber, int episodeNumber)
        {
            var serie = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAsync(id);
            if (serie == null)
                return NotFound();

            var season = serie.SerieSeasons.Where(x => x.SeasonNumber == seasonNumber).FirstOrDefault();
            if (season == null)
                return NotFound();

            var episode = season.SeasonEpisodes.Where(x => x.EpisodeNumber == episodeNumber).FirstOrDefault();
            if (episode == null)
                return NotFound();

            return _mapper.Map<SimpleEpisodeDTO>(episode);
        }

        /// <summary>
        /// Get actors of target episode
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <param name="episodeNumber"></param>
        /// <returns>List of actors</returns>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///         "id": "1",
        ///         "seasonNumber": "1",
        ///         "episodeNumber": "1"
        ///     }
        /// <response code="200">When list of actors was returned</response>
        /// <response code="404">If any object doesn't exist</response>
        [HttpGet("{id}/seasons/{seasonNumber}/episodes/{episodeNumber}/actors")]
        public async Task<ActionResult<List<SimpleActorDTO>>> GetActorsFromEpisode(int id, int seasonNumber, int episodeNumber)
        {
            var serie = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAndActorsAsync(id);
            if (serie == null)
                return NotFound();

            var season = serie.SerieSeasons.Where(x => x.SeasonNumber == seasonNumber).FirstOrDefault();
            if (season == null)
                return NotFound();

            var episode = season.SeasonEpisodes.Where(x => x.EpisodeNumber == episodeNumber).FirstOrDefault();
            if (episode == null)
                return NotFound();

            List<Actor> actors = episode.EpisodeActors;

            return Ok(_mapper.Map<IEnumerable<SimpleActorDTO>>(actors));
        }

        /// <summary>
        /// Add new series
        /// </summary>
        /// <returns>Add new series</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "serieName": "Breaking Bad"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(SerieCreateDto newSerie)
        {
            var createdSerie = _mapper.Map<Serie>(newSerie);
            await _seriesRepository.CreateAsync(createdSerie);
            if (createdSerie == null) return BadRequest();
            var entity = await _seriesRepository.RetrieveAsync(createdSerie.SerieId);
            var readDto = _mapper.Map<SimpleSerieDTO>(entity);
            return CreatedAtRoute(nameof(GetSerieById), new { Id = readDto.SerieId }, readDto);
        }

        /// <summary>
        /// Delete series
        /// </summary>
        /// <returns>Delete series</returns>
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
            var result = await _seriesRepository.DeleteAsync(id);
            if (result == null) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Update series name
        /// </summary>
        /// <returns>Update series name</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "op": "replace",
        ///        "path": "SerieName",
        ///        "value": "NewName"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>

        //Patch api/series/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<SerieUpdateDto> patchDoc)
        {
            var modelFromRepo = await _seriesRepository.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<SerieUpdateDto>(modelFromRepo);
            patchDoc.ApplyTo(entityToPatch, ModelState);
            if (!TryValidateModel(entityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(entityToPatch, modelFromRepo);
            await _seriesRepository.UpdateAsync(modelFromRepo);
            //await _repository.SaveChanges();
            return NoContent();
        }
    }
}

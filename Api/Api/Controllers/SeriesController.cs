namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IBaseRepository<Genre> _genresRepository;
        private readonly IEpisodesRepository _episodesRepository;
        private readonly IMapper _mapper;

        public SeriesController(ISeriesRepository seriesRepository, IBaseRepository<Genre> genresRepository, IEpisodesRepository episodesRepository,  IMapper mapper)
        {
            _seriesRepository = seriesRepository;
            _genresRepository = genresRepository;
            _episodesRepository = episodesRepository;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSerialById(int id)
        {
            var serial = await _seriesRepository.RetrieveAsync(id);
            if (serial == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SimpleSerieDTO>(serial));
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
        [HttpPut("id/genres/{genreId}")]
        public async Task<IActionResult> AddGenreToSerial(int id, int genreId)
        {
            var serial = await _seriesRepository.RetrieveAsync(id);
            if (serial == null)
            {
                return NotFound();
            }
            var genre = await _genresRepository.RetrieveAsync(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            serial.SerieGenres.Add(genre);
            genre.GenreSerie.Add(serial);
            await _seriesRepository.UpdateAsync(serial.SerieId, serial);
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
        [HttpDelete("id/genres/{genreId}")]
        public async Task<IActionResult> DeleteGenreToSerial(int id, int genreId)
        {
            var serial = await _seriesRepository.RetrieveSerieWithGenresAsync(id);
            if (serial == null)
            {
                return NotFound();
            }
            var genre = await _genresRepository.RetrieveAsync(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            serial.SerieGenres.Remove(genre);
            await _seriesRepository.UpdateAsync(serial.SerieId, serial);
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
        public async Task<IActionResult> GetSerialWithSeasonsById(int id)
        {
            var serial = await _seriesRepository.RetrieveWithSeasonsAndEpisodesAsync(id);
            if (serial == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SerieWithSeasonsDTO>(serial));
        }




        /// <summary>
        /// Get episodes of target season of the serie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seasonNumber"></param>
        /// <returns>List of episodes </returns>
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


        [HttpPost("{id}/seasons/{seasonNumber}/episodes/{episodeId}")]
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
    }
}

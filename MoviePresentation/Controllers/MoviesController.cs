using Microsoft.AspNetCore.Mvc;
using MovieCore.Models.DTOs;
using MovieServiceContracts;

namespace MoviePresentation.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public MoviesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie(bool includeActors) =>
                Ok(await _serviceManager.Movies.GetAllMoviesAsync(includeActors));

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id, bool includeActors)
        {
            return Ok(await _serviceManager.Movies.GetMovieAsync(id, includeActors));
        }

        // GET: api/Movies/5/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetail(int id)
        {
            return Ok(await _serviceManager.Movies.GetMovieWithDetailsAsync(id));
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            return await _serviceManager.Movies.UpdateMovieAsync(id, dto)
                ? NoContent()
                : NotFound();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieDetailDto>> PostMovie(MovieCreateDto dto)
        {
            var movieDto = await _serviceManager.Movies.AddMovieAsync(dto);
            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            return await _serviceManager.Movies.RemoveMovieAsync(id)
                ? NoContent()
                : NotFound();
        }
    }
}

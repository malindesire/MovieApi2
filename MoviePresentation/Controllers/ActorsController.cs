using Microsoft.AspNetCore.Mvc;
using MovieServiceContracts;

namespace MoviePresentation.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ActorsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // POST: api/Movies/5/Actors/3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
        public async Task<ActionResult> AddActorToMovie(int movieId, int actorId)
        {
            await _serviceManager.Actors.AddActorToMovieAsync(movieId, actorId);

            return Ok($"Actor with ID {actorId} is added to movie with ID {movieId}");
        }
    }
}

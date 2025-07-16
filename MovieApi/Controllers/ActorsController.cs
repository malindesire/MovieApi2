using Microsoft.AspNetCore.Mvc;
using MovieCore.DomainContracts;

namespace MovieApi.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ActorsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        // POST: api/Movies/5/Actors/3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
        public async Task<ActionResult> AddActorToMovie(int movieId, int actorId)
        {
            _uow.Actors.AddActorToMovie(movieId, actorId);
            await _uow.CompleteAsync();

            return Ok($"Actor with ID {actorId} is added to movie with ID {movieId}");
        }
    }
}

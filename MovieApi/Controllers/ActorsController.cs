using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ActorsController(MovieContext context)
        {
            _context = context;
        }

        // POST: api/Movies/5/Actors/3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
        public async Task<ActionResult> AddActorToMovie(int movieId, int actorId)
        {   
            var movie = await _context.Movies.
                Include(m => m.Actors).
                FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                return NotFound($"Movie with ID {movieId} not found.");
            }

            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == actorId);
            if (actor == null)
            {
                return NotFound($"Actor with ID {actorId} not found.");
            }

            // Check if the actor is already associated with the movie
            if (movie.Actors.Any(a => a.Id == actorId))
            {
                return BadRequest($"Actor with ID {actorId} is already associated with Movie ID {movieId}.");
            }

            // Add the actor to the movie's actors collection
            movie.Actors.Add(actor);

            _context.Entry(movie).State = EntityState.Modified;
            _context.Entry(actor).State = EntityState.Unchanged; // Ensure the actor's state remains unchanged

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}

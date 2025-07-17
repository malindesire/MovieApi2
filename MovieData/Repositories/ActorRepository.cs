using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieData.Data;

namespace MovieData.Repositories
{
    public class ActorRepository : IActorRepository
    {
        protected MovieContext _context { get; }
        public ActorRepository(MovieContext context)
        {
            _context = context;
        }
        public void AddActorToMovie(int movieId, int actorId)
        {
            var movie = _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
            {
                throw new ArgumentException($"Movie with ID {movieId} not found.");
            }

            var actor = _context.Actors.FirstOrDefault(a => a.Id == actorId);
            if (actor == null)
            {
                throw new ArgumentException($"Actor with ID {actorId} not found.");
            }

            if (movie.Actors.Any(a => a.Id == actorId))
            {
                throw new InvalidOperationException($"Actor with ID {actorId} is already associated with Movie ID {movieId}.");
            }

            movie.Actors.Add(actor);
        }
    }
}

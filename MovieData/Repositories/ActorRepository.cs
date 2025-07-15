using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories
{
    public class ActorRepository : IActorRepository
    {
        protected DbSet<Movie> DbSet { get; }
        public ActorRepository(MovieContext context)
        {
            DbSet = context.Set<Movie>();
        }
        public void AddActorToMovie(int movieId, int actorId)
        {
            var movie = DbSet
                .Include(m => m.Actors)
                .FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
            {
                throw new ArgumentException($"Movie with ID {movieId} not found.");
            }

            var actor = movie.Actors.FirstOrDefault(a => a.Id == actorId);
            if (actor == null)
            {
                throw new ArgumentException($"Actor with ID {actorId} not found in Movie ID {movieId}.");
            }

            if (movie.Actors.Any(a => a.Id == actorId))
            {
                throw new InvalidOperationException($"Actor with ID {actorId} is already associated with Movie ID {movieId}.");
            }

            movie.Actors.Add(actor);
        }
    }
}

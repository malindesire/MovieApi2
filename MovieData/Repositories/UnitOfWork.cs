using MovieCore.DomainContracts;
using MovieData.Data;

namespace MovieData.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _context;

        public IMovieRepository Movies { get; }
        public IActorRepository Actors { get; }
        public IReviewRepository Reviews { get; }

        public UnitOfWork(MovieContext context)
        {
            _context = context;
            Movies = new MovieRepository(context);
            Actors = new ActorRepository(context);
            Reviews = new ReviewRepository(context);
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}

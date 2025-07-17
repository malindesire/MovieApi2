using MovieCore.DomainContracts;
using MovieServiceContracts;

namespace MovieServices
{
    public class ActorService : IActorService
    {
        private IUnitOfWork _uow;

        public ActorService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task AddActorToMovieAsync(int movieId, int actorId)
        {
            _uow.Actors.AddActorToMovie(movieId, actorId);
            await _uow.CompleteAsync();
        }
    }
}

namespace MovieCore.DomainContracts
{
    public interface IActorRepository
    {
        void AddActorToMovie(int movieId, int actorId);
    }
}

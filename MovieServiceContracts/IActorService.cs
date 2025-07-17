namespace MovieServiceContracts
{
    public interface IActorService
    {
        Task AddActorToMovieAsync(int movieId, int actorId);
    }
}

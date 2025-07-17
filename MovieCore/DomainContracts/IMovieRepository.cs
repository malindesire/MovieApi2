using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync(bool include = false);
        Task<Movie?> GetAsync(int id, bool include = false);
        Task<Movie?> GetWithDetailsAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Remove(Movie movie);
    }
}

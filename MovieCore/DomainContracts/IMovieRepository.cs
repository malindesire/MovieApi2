using MovieCore.Models.Entities;
using MovieCore.Requests;

namespace MovieCore.DomainContracts
{
    public interface IMovieRepository
    {
        Task<(IEnumerable<Movie>, PaginationMetadata)> GetAllAsync(bool include, MoviePaginationParamaters paginationParamaters);
        Task<Movie?> GetAsync(int id, bool include);
        Task<Movie?> GetWithDetailsAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Remove(Movie movie);
    }
}

using MovieCore.Models.DTOs;
using MovieCore.Requests;

namespace MovieServiceContracts
{
    public interface IMovieService
    {
        Task<(IEnumerable<MovieDto>, PaginationMetadata)> GetAllMoviesAsync(bool includeActors, MoviePaginationParamaters paginationParamaters);
        Task<MovieDto> GetMovieAsync(int id, bool includeActors);
        Task<MovieDetailDto> GetMovieWithDetailsAsync(int id);
        Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto);
        Task<MovieDetailDto> AddMovieAsync(MovieCreateDto dto);
        Task<bool> RemoveMovieAsync(int id);
    }
}
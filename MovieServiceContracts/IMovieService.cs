using MovieCore.Models.DTOs;

namespace MovieServiceContracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync(bool includeActors);
        Task<MovieDto> GetMovieAsync(int id, bool includeActors);
        Task<MovieDetailDto> GetMovieWithDetailsAsync(int id);
        Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto);
        Task<MovieDetailDto> AddMovieAsync(MovieCreateDto dto);
        Task<bool> RemoveMovieAsync(int id);
    }
}
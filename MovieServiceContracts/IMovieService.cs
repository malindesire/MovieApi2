using MovieCore.Models.DTOs;

namespace MovieServiceContracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync(bool includeActors);
        Task<MovieDto> GetMovieAsync(int id, bool includeActors);
        Task<MovieDetailDto> GetMovieWithDetails(int id);
        Task UpdateMovie(int id, MovieUpdateDto dto);
        Task<MovieDetailDto> AddMovie(MovieCreateDto dto);
        Task RemoveMovie(int id);
    }
}
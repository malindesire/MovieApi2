using MovieCore.DomainContracts;
using MovieCore.Models.DTOs;
using MovieServiceContracts;

namespace MovieServices
{
    public class MovieService : IMovieService
    {
        private IUnitOfWork _uow;

        public MovieService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync(bool includeActors)
        {
            var movies = await _uow.Movies.GetAllAsync(includeActors);
            
            if (movies == null || !movies.Any())
            {
                return Enumerable.Empty<MovieDto>();
            }

            return movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genre = m.Genre,
                Duration = m.Duration,
                Actors = includeActors ? m.Actors
                    .Select(a => new ActorDto
                        {
                            Id = a.Id,
                            FullName = $"{a.FirstName} {a.LastName}",
                            BirthYear = a.BirthYear
                        })
                    .ToList() : null
            });
        }

        public Task<MovieDto> GetMovieAsync(int id, bool includeActors)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailDto> GetMovieWithDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovie(int id, MovieUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailDto> AddMovie(MovieCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
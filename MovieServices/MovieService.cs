using MovieCore.DomainContracts;
using MovieCore.Models.DTOs;
using MovieCore.Models.Entities;
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

        public async Task<MovieDto> GetMovieAsync(int id, bool includeActors)
        {
            var movie = await _uow.Movies.GetAsync(id, includeActors);

            if (movie == null)
            {
                return null;
            }

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Actors = includeActors ? movie.Actors
                    .Select(a => new ActorDto
                        {
                            Id = a.Id,
                            FullName = $"{a.FirstName} {a.LastName}",
                            BirthYear = a.BirthYear
                        })
                    .ToList() : null
            };
        }

        public async Task<MovieDetailDto> GetMovieWithDetailsAsync(int id)
        {
            var movie = await _uow.Movies.GetWithDetailsAsync(id);

            if (movie == null)
            {
                return null;
            }

            return new MovieDetailDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Language = movie.MovieDetails.Language,
                Budget = movie.MovieDetails.Budget,
                Synopsis = movie.MovieDetails.Synopsis,
                AverageRating = movie.Reviews.Any() ? movie.Reviews.Average(r => r.Rating) : 0.0,
                Reviews = movie.Reviews
                    .Select(r => new ReviewDto
                        {
                            Id = r.Id,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            ReviewerName = r.ReviewerName
                        })
                    .ToList(),
                Actors = movie.Actors
                    .Select(a => new ActorDto
                        {
                            Id = a.Id,
                            FullName = $"{a.FirstName} {a.LastName}",
                            BirthYear = a.BirthYear
                        })
                    .ToList()
            };

        }

        public async Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto)
        {
            var movie = await _uow.Movies.GetWithDetailsAsync(id);

            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found.");
            }

            // Update the movie properties from the DTO
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Genre = dto.Genre;
            movie.Duration = dto.Duration;
            movie.MovieDetails.Synopsis = dto.Synopsis;
            movie.MovieDetails.Language = dto.Language;
            movie.MovieDetails.Budget = dto.Budget;

            _uow.Movies.Update(movie);
            await _uow.CompleteAsync();

            return true;
        }

        public async Task<MovieDetailDto> AddMovieAsync(MovieCreateDto dto)
        {
            var movie = new Movie 
            {
                Title = dto.Title,
                Year = dto.Year,
                Genre = dto.Genre,
                Duration = dto.Duration,
                MovieDetails = new MovieDetails
                {
                    Language = dto.Language,
                    Budget = dto.Budget,
                    Synopsis = dto.Synopsis
                }
            };

            _uow.Movies.Add(movie);
            await _uow.CompleteAsync();

            return new MovieDetailDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Language = movie.MovieDetails.Language,
                Budget = movie.MovieDetails.Budget,
                Synopsis = movie.MovieDetails.Synopsis,
            };
        }

        public async Task<bool> RemoveMovieAsync(int id)
        {
            var movie = await _uow.Movies.GetAsync(id, false);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found.");
            }

            _uow.Movies.Remove(movie);
            await _uow.CompleteAsync();

            return true;
        }
    }
}
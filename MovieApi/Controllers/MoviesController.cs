
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieData.Data;
using MovieCore.Models.DTOs;
using MovieCore.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie(bool includeActors)
        {
           var query = includeActors ? _context.Movies.Include(m => m.Actors).AsQueryable() : _context.Movies.AsQueryable();

           var movies = query
                .Select((m) => new MovieDto 
                   {
                        Id = m.Id,
                        Title = m.Title,
                        Year = m.Year,
                        Genre = m.Genre,
                        Duration = m.Duration,
                        Actors = includeActors ? m.Actors.Select(a => new ActorDto
                        {
                            Id = a.Id,
                            FullName = $"{a.FirstName} {a.LastName}",
                            BirthYear = a.BirthYear,
                        }) : null
                });

           return Ok(await movies.ToListAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id, bool includeActors)
        {
            var query = includeActors ? _context.Movies.Include(m => m.Actors).AsQueryable() : _context.Movies.AsQueryable();

            var movie = query
                .Select(m => new MovieDto
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Year = m.Year,
                        Genre = m.Genre,
                        Duration = m.Duration,
                        Actors = includeActors ? m.Actors.Select(a => new ActorDto
                        {
                            Id = a.Id,
                            FullName = $"{a.FirstName} {a.LastName}",
                            BirthYear = a.BirthYear,
                        }) : null
                })
                .FirstOrDefaultAsync(m => id == m.Id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(await movie);
        }

        // GET: api/Movies/5/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetail(int id)
        {
            var movieDetails = _context.Movies.
                Select(m => new MovieDetailDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre,
                    Duration = m.Duration,
                    Language = m.MovieDetails.Language,
                    Budget = m.MovieDetails.Budget,
                    AverageRating = m.Reviews.Any() ? m.Reviews.Average(r => r.Rating) : 0.0,
                    Synopsis = m.MovieDetails.Synopsis,
                    Reviews = m.Reviews.Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        ReviewerName = r.ReviewerName
                    }).ToList(),
                    Actors = m.Actors.Select(a => new ActorDto
                    {
                        Id = a.Id,
                        FullName = $"{a.FirstName} {a.LastName}",
                        BirthYear = a.BirthYear,
                    }).ToList()
                }).
                FirstOrDefaultAsync(m => id == m.Id);

            if (movieDetails == null) return NotFound();

            return Ok(await movieDetails);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _context.Movies.
                Include(m => m.MovieDetails).
                FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null) return NotFound();

            // Update the movie properties from the DTO
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Genre = dto.Genre;
            movie.Duration = dto.Duration;
            movie.MovieDetails.Synopsis = dto.Synopsis;
            movie.MovieDetails.Language = dto.Language;
            movie.MovieDetails.Budget = dto.Budget;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Genre = dto.Genre,
                Duration = dto.Duration,
                MovieDetails = new MovieDetails
                {
                    Synopsis = dto.Synopsis,
                    Language = dto.Language,
                    Budget = dto.Budget
                }
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
            };

            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}

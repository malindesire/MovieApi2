using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieData.Data;
using MovieCore.Models.DTOs;
using MovieCore.Models.Entities;
using MovieCore.DomainContracts;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie(bool includeActors)
        {
            var movies = await _uow.Movies.GetAllAsync();
            if (movies == null || !movies.Any())
            {
                return NotFound();
            }

            var dtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genre = m.Genre,
                Duration = m.Duration,
            }).ToList();

            return Ok(dtos);

           //var query = includeActors ? _context.Movies.Include(m => m.Actors).AsQueryable() : _context.Movies.AsQueryable();

           //var movies = query
           //     .Select((m) => new MovieDto 
           //        {
           //             Id = m.Id,
           //             Title = m.Title,
           //             Year = m.Year,
           //             Genre = m.Genre,
           //             Duration = m.Duration,
           //             Actors = includeActors ? m.Actors.Select(a => new ActorDto
           //             {
           //                 Id = a.Id,
           //                 FullName = $"{a.FirstName} {a.LastName}",
           //                 BirthYear = a.BirthYear,
           //             }) : null
           //     });

           //return Ok(await movies.ToListAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id, bool includeActors)
        {
            if (!await _uow.Movies.AnyAsync(id))
            {
                return NotFound();
            }

            var movie = await _uow.Movies.GetAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var dto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
            };

            return Ok(dto);
            //    var query = includeActors ? _context.Movies.Include(m => m.Actors).AsQueryable() : _context.Movies.AsQueryable();

            //    var movie = query
            //        .Select(m => new MovieDto
            //            {
            //                Id = m.Id,
            //                Title = m.Title,
            //                Year = m.Year,
            //                Genre = m.Genre,
            //                Duration = m.Duration,
            //                Actors = includeActors ? m.Actors.Select(a => new ActorDto
            //                {
            //                    Id = a.Id,
            //                    FullName = $"{a.FirstName} {a.LastName}",
            //                    BirthYear = a.BirthYear,
            //                }) : null
            //        })
            //        .FirstOrDefaultAsync(m => id == m.Id);

            //    if (movie == null)
            //    {
            //        return NotFound();
            //    }

            //    return Ok(await movie);
        }

        // GET: api/Movies/5/details
       [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetail(int id)
        {
            if (!await _uow.Movies.AnyAsync(id))
            {
                return NotFound();
            }

            var movie = await _uow.Movies.GetWithDetailsAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var dto = new MovieDetailDto
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
                Reviews = movie.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewerName = r.ReviewerName
                }).ToList(),
                Actors = movie.Actors.Select(a => new ActorDto
                {
                    Id = a.Id,
                    FullName = $"{a.FirstName} {a.LastName}",
                    BirthYear = a.BirthYear
                }).ToList()
            };

            return Ok(dto);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _uow.Movies.GetWithDetailsAsync(id);

            if (movie is null) return NotFound();


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

            return NoContent();
        }

        //// POST: api/Movies
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto dto)
        //{
        //    var movie = new Movie
        //    {
        //        Title = dto.Title,
        //        Year = dto.Year,
        //        Genre = dto.Genre,
        //        Duration = dto.Duration,
        //        MovieDetails = new MovieDetails
        //        {
        //            Synopsis = dto.Synopsis,
        //            Language = dto.Language,
        //            Budget = dto.Budget
        //        }
        //    };

        //    _context.Movies.Add(movie);
        //    await _context.SaveChangesAsync();

        //    var movieDto = new MovieDto
        //    {
        //        Id = movie.Id,
        //        Title = movie.Title,
        //        Year = movie.Year,
        //        Genre = movie.Genre,
        //        Duration = movie.Duration,
        //    };

        //    return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        //}

        //// DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMovie(int id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MovieExists(int id)
        //{
        //    return _context.Movies.Any(e => e.Id == id);
        //}
    }
}

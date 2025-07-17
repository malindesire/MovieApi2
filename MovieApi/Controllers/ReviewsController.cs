using Microsoft.AspNetCore.Mvc;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs;
using MovieData.Data;

namespace MovieApi.Controllers
{
    [Route("api/movies/{movieId}/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ReviewsController(MovieContext context, IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/movies/{movieId}/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(int movieId)
        {
            if (!await _uow.Movies.AnyAsync(movieId))
            {
                return NotFound($"Movie with ID {movieId} not found.");
            }

            var reviews = await _uow.Reviews.GetAllAsync(movieId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound($"No reviews found for movie with ID {movieId}.");
            }

            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating,

            }).ToList();


            return Ok(reviewDtos);
        }
    }
}

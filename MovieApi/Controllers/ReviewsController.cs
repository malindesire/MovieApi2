using Microsoft.AspNetCore.Mvc;
using MovieCore.Models.DTOs;
using MovieServiceContracts;

namespace MovieApi.Controllers
{
    [Route("api/movies/{movieId}/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ReviewsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/movies/{movieId}/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(int movieId) =>
            Ok(await _serviceManager.Reviews.GetReviewsAsync(movieId));
    }
}

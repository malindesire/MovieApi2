using MovieCore.DomainContracts;
using MovieCore.Models.DTOs;
using MovieServiceContracts;

namespace MovieServices
{
    public class ReviewService : IReviewService
    {
        private IUnitOfWork _uow;

        public ReviewService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId)
        {
            if (!await _uow.Movies.AnyAsync(movieId))
            {
                return Enumerable.Empty<ReviewDto>();
            }

            var reviews = await _uow.Reviews.GetAllAsync(movieId);
            if (reviews == null || !reviews.Any())
            {
                return null;
            }

            return reviews
                .Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        ReviewerName = r.ReviewerName,
                        Comment = r.Comment,
                        Rating = r.Rating,

                    })
                .ToList();
        }
    }
}

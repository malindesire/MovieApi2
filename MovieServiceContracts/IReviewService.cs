using MovieCore.Models.DTOs;

namespace MovieServiceContracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId);
    }
}

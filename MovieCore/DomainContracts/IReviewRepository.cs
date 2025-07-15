using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync(int movieId);
    }
}

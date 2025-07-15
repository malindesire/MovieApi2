using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        protected DbSet<Review> DbSet { get; }
        public ReviewRepository(MovieContext context)
        {
            DbSet = context.Set<Review>();
        }
        public async Task<IEnumerable<Review>> GetAllAsync(int movieId) => await DbSet.Where(r => r.MovieId == movieId).ToListAsync();
    }
}

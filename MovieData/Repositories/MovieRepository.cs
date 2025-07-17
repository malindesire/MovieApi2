using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        protected DbSet<Movie> DbSet { get; }
        public MovieRepository(MovieContext context)
        {
            DbSet = context.Set<Movie>();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(bool include)
        {
            return include ?
                  await DbSet.Include(m => m.Actors).ToListAsync() :
                  await DbSet.ToListAsync();
        }

        public async Task<Movie?> GetAsync(int id, bool include)
        {
             return include ?
                await DbSet.Include(m => m.Actors)
                            .FirstOrDefaultAsync(m => m.Id == id) :
                await DbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie?> GetWithDetailsAsync(int id) => await DbSet
            .Include(m => m.MovieDetails)
            .Include(m => m.Reviews)
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> AnyAsync(int id) => await DbSet.AnyAsync(m => m.Id == id);

        public void Add(Movie movie) => DbSet.Add(movie);

        public void Update(Movie movie) => DbSet.Update(movie);

        public void Remove(Movie movie) => DbSet.Remove(movie);


    }
}

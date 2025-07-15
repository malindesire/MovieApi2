using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;
using System.Collections.Generic;

namespace MovieData.Repositories
{
    internal class MovieRepository : IMovieRepository
    {
        protected DbSet<Movie> DbSet { get; }
        public MovieRepository(MovieContext context) 
        {
            DbSet = context.Set<Movie>();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync() => await DbSet.ToListAsync();

        public async Task<Movie?> GetAsync(int id) => await DbSet.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> AnyAsync(int id) => await DbSet.AnyAsync(m => m.Id == id);

        public void Add(Movie movie) => DbSet.Add(movie);

        public void Update(Movie movie) => DbSet.Update(movie);

        public void Remove(Movie movie) => DbSet.Remove(movie);
    }
}

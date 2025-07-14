using Microsoft.EntityFrameworkCore;
using MovieCore.Models.Entities;

namespace MovieData.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Actor> Actors { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
    }
}

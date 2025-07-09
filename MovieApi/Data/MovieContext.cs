using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<MovieApi.Models.Entities.Movie> Movies { get; set; } = default!;
        public DbSet<MovieApi.Models.Entities.Actor> Actors { get; set; } = default!;
        public DbSet<MovieApi.Models.Entities.Review> Reviews { get; set; } = default!;
    }
}

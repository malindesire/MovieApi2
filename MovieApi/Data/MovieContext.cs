using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieCore.Models.Entities;

namespace MovieApi.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<MovieCore.Models.Entities.Movie> Movies { get; set; } = default!;
        public DbSet<MovieCore.Models.Entities.Actor> Actors { get; set; } = default!;
        public DbSet<MovieCore.Models.Entities.Review> Reviews { get; set; } = default!;
    }
}

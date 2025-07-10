using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(m => m.Year)
                .IsRequired()
                .HasMaxLength(2025);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(m => m.Duration)
                .IsRequired()
                .HasMaxLength(600);

            builder.HasOne(m => m.MovieDetails)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetails>(md => md.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Actors)
                    .WithMany(a => a.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActorMovie",
                        j => j
                            .HasOne<Actor>()
                            .WithMany()
                            .HasForeignKey("ActorId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Movie>()
                            .WithMany()
                            .HasForeignKey("MovieId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("ActorId", "MovieId");
                            j.ToTable("ActorMovie");
                        });
        }
    }
}

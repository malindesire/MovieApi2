using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data
{
    public class SeedData
    {
        private static Faker faker = new Faker("sv");
        internal static async Task InitAsync(MovieContext context)
        {
            if (await context.Movie.AnyAsync()) return;

            IEnumerable<Movie> movies = GetMovies(30);
            await context.Movie.AddRangeAsync(movies);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Movie> GetMovies(int qty)
        {
            IEnumerable<Actor> actors = GetActors(faker.Random.Int(1, 5));
            IEnumerable<Review> reviews = GetReviews(faker.Random.Int(1, 5));

            Console.WriteLine($"Actors: {actors.Count()}, Reviews: {reviews.Count()}");

            var movies = faker.Make(qty, () =>
            {
                var movie = new Movie
                {
                    Title = faker.Lorem.Sentence(3, 5),
                    Year = faker.Date.Past(20).Year,
                    Genre = faker.PickRandom(new[] { "Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Romance" }),
                    Duration = faker.Random.Int(60, 180),
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = faker.Lorem.Paragraph(),
                        Language = faker.PickRandom(new[] { "English", "Spanish", "French", "German", "Chinese" }),
                        Budget = faker.Finance.Amount(1000000, 200000000)
                    },
                    Actors = actors.ToList(),
                    Reviews = reviews.ToList()
                };
                return movie;
            });

            return movies;
        }

        private static IEnumerable<Review> GetReviews(int qty)
        {
            var reviews = faker.Make(qty, () =>
            {
                return new Review
                {
                    ReviewerName = faker.Name.FullName(),
                    Comment = faker.Lorem.Sentence(10, 20),
                    Rating = faker.Random.Int(1, 5)
                };
            });

            return reviews;
        }

        private static IEnumerable<Actor> GetActors(int qty)
        {
            var actors = faker.Make(qty, () =>
            {
                return new Actor
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    BirthYear = faker.Date.Past(50).Year
                };
            });

            return actors;
        }
    }
}

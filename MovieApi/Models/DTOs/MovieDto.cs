namespace MovieApi.Models.DTOs
{
    public record MovieDto
    {
        public required string Title { get; init; }
        public required int Year { get; init; }
        public required string Genre { get; init; }
        public required int Duration { get; init; }
        public required string Language { get; init; }
        public required decimal Budget { get; init; }
        public required double AverageRating { get; init; }
        public required string[] Actors { get; init; } = [];
    }
}

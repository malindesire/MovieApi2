namespace MovieCore.Models.DTOs
{
    public record MovieDetailDto
    {
        public required int Id { get; init; }
        public required string Title { get; init; }
        public required int Year { get; init; }
        public required string Genre { get; init; }
        public required int Duration { get; init; }
        public required string Language { get; init; }
        public required decimal Budget { get; init; }
        public required string Synopsis { get; init; } = string.Empty;
        public double AverageRating { get; init; }
        public List<ActorDto> Actors { get; init; } = new List<ActorDto>();
        public List<ReviewDto> Reviews { get; init; } = new List<ReviewDto>();
    }
}

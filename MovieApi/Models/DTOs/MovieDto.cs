namespace MovieApi.Models.DTOs
{
    public record MovieDto
    {
        public required int Id { get; init; }
        public required string Title { get; init; }
        public required int Year { get; init; }
        public required string Genre { get; init; }
        public required int Duration { get; init; }
    }
}

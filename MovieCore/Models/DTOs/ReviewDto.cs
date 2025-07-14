namespace MovieCore.Models.DTOs
{
    public record ReviewDto
    {
        public required int Id { get; init; }
        public required string ReviewerName { get; init; }
        public required string Comment { get; init; }
        public required int Rating { get; init; }
    }
}

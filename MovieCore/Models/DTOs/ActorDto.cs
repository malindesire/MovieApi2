namespace MovieCore.Models.DTOs
{
    public record ActorDto
    {
        public required int Id { get; init; }
        public required string FullName { get; init; }
        public required int BirthYear { get; init; }
    }
}

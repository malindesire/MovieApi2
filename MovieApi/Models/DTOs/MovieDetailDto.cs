namespace MovieApi.Models.DTOs
{
    public record MovieDetailDto : MovieDto
    {
        public required string Synopsis { get; init; } = string.Empty;
        public List<ActorDto> Actors { get; init; } = new List<ActorDto>();
        public List<ReviewDto> Reviews { get; init; } = new List<ReviewDto>();
    }
}

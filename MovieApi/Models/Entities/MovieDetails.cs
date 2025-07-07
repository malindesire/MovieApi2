namespace MovieApi.Models.Entities
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = null!;
        public string Language { get; set; } = null!;
        public decimal Budget { get; set; }

        //Foreign keys
        public int MovieId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; } = null!;
    }
}

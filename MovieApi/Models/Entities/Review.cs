namespace MovieApi.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rating { get; set; } // Rating between 1 and 5
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!; // Navigation property to the Movie entity
    }
}

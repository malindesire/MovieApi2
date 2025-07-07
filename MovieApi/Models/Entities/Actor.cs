namespace MovieApi.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public int BirthYear { get; set; }
        public ICollection<Movie> Movies { get; set; } = [];
    }
}

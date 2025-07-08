using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public record MovieManipulateDto
    {
        [Required(ErrorMessage = "Movie title is a required field")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publication year is a required field")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Movie genre is a required field")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie duration is a required field")]
        public int Duration { get; set; } // Duration in minutes

        [Required(ErrorMessage = "Movie synopsis is a required field")]
        [StringLength(1000, ErrorMessage = "Synopsis cannot exceed 1000 characters")]
        public string Synopsis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie language is a required field")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie budget is a required field")]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a non-negative value")]
        public decimal Budget { get; set; } // Budget in USD
    }
}

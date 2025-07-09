using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public record MovieManipulateDto
    {
        [Required(ErrorMessage = "Movie title is a required field")]
        [StringLength(60, ErrorMessage = "Title cannot exceed 60 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publication year is a required field")]
        [Range(1888, 2025, ErrorMessage = "Year must be a valid year (after 1888 and before 2025)")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Movie genre is a required field")]
        [StringLength(30, ErrorMessage = "Genre cannot exceed 30 characters")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie duration is a required field")]
        [Range(1, 600, ErrorMessage = "Duration must be between 1 and 600 minutes")]
        public int Duration { get; set; } // Duration in minutes

        [Required(ErrorMessage = "Movie synopsis is a required field")]
        [StringLength(1000, ErrorMessage = "Synopsis cannot exceed 1000 characters")]
        public string Synopsis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie language is a required field")]
        [StringLength(30, ErrorMessage = "Language cannot exceed 30 characters")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie budget is a required field")]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a non-negative value")]
        public decimal Budget { get; set; } // Budget in USD
    }
}

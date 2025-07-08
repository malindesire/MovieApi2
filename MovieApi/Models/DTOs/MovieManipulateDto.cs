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
    }
}

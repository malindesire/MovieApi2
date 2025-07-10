using System.ComponentModel.DataAnnotations;

namespace MovieApi.Validations
{
    public class YearNotInFuture : ValidationAttribute
    {
        public int MinYear = 1895;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not int year)
            {
                return new ValidationResult("Year must be an integer.");
            }

            var currentYear = DateTime.Now.Year;

            if (year < MinYear || year > currentYear)
            {
                return new ValidationResult($"Year must be between {MinYear} and {currentYear}.");
            }

            return ValidationResult.Success;
        }
    }
}

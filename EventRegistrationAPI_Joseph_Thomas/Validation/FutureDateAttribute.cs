using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_Joseph_Thomas.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date > DateTime.Now)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? "Event date must be in the future.");
        }

    }
}

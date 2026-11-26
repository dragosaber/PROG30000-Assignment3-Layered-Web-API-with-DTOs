using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_Joseph_Thomas.DTOs
{
    public class CreateRegistrationDto
    {
        [Required(ErrorMessage = "Student name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
    }
}

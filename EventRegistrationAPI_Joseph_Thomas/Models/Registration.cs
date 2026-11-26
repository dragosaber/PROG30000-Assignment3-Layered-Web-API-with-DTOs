using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_Joseph_Thomas.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        public int EventId { get; set; }

        [Required]
        public string? StudentName { get; set; }
        [Required]
        public string? Email { get; set; }

        public DateTime RegisteredOn { get; set; }

        public Event? Event { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_Joseph_Thomas.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required, StringLength(100)]
        public string? Title { get; set; }

        [Required]
        public string? Location { get; set; }

        public DateTime EventDate { get; set; }

        public int Capacity { get; set; }

        public List<Registration>? Registrations { get; set; }
    }
}

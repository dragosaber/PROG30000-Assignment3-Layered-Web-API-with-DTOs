using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_Joseph_Thomas.Models;

namespace EventRegistrationAPI_Joseph_Thomas.Data
{
    public class AppDbContext : DbContext
    {
      

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>().HasData(
            new Event { EventId = 1, Title = "Orientation Mixer", Location = "Main Hall", EventDate = new DateTime(2025, 9, 5, 18, 0, 0), Capacity = 100 },
            new Event { EventId = 2, Title = "Career Fair", Location = "Gym", EventDate = new DateTime(2027, 3, 15, 10, 0, 0), Capacity = 100 },
            new Event { EventId = 3, Title = "Git Workshop", Location = "Lab 204", EventDate = new DateTime(2027, 2, 10, 14, 0, 0), Capacity = 2 },
            new Event { EventId = 4, Title = "Hackathon", Location = "Innovation Hub", EventDate = new DateTime(2027, 4, 20, 9, 0, 0), Capacity = 50 }
        );

        modelBuilder.Entity<Registration>().HasData(
            new Registration { RegistrationId = 1, EventId = 3, StudentName = "Joseph Thomas", Email = "jojo@gmail.com", RegisteredOn = new DateTime(2026, 11, 1) },
            new Registration { RegistrationId = 2, EventId = 3, StudentName = "Benjamin Franklin", Email = "1@gmail.com", RegisteredOn = new DateTime(2026, 11, 2) }
        );
    }

    }
}

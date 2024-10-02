using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Data
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GuestBooking> Guests { get; set; }
        public DbSet<Staff> Staff { get; set; }

        public DbSet<Staffing> Staffing { get; set; }
        public DbSet<Staffing> StaffRole { get; set; }

        private IWebHostEnvironment HostEnv { get; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options,
                               IWebHostEnvironment env) : base(options)
        {
            HostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("thamco.events");

            builder.Entity<GuestBooking>()
                   .HasKey(b => new { b.CustomerId, b.EventId });

            builder.Entity<Customer>()
                   .HasMany(c => c.Bookings)
                   .WithOne(b => b.Customer)
                   .HasForeignKey(b => b.CustomerId);

            builder.Entity<Event>()
                   .HasMany(e => e.Bookings)
                   .WithOne(b => b.Event)
                   .HasForeignKey(b => b.EventId);

            builder.Entity<Event>()
                   .Property(e => e.TypeId)
                   .IsFixedLength();

            builder.Entity<Staffing>()
               .HasKey(a => new { a.StaffId, a.EventId });

            builder.Entity<Staff>()
                .HasMany(s => s.StaffingList)
                        .WithOne(a => a.Staff)
                        .HasForeignKey(a => a.StaffId);

            // seed data for debug / development testing
            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                builder.Entity<Customer>().HasData(
                    new Customer { Id = 1, Surname = "Robertson", FirstName = "Robert", Email = "bob@example.com" },
                    new Customer { Id = 2, Surname = "Thornton", FirstName = "Betty", Email = "betty@example.com" },
                    new Customer { Id = 3, Surname = "Jellybeans", FirstName = "Jin", Email = "jin@example.com" }
                );

                builder.Entity<Event>().HasData(
                    new Event { Id = 1, Title = "Bob's Big 50", Date = new DateTime(2016, 4, 12), Duration = new TimeSpan(6, 0, 0), TypeId = "PTY" },
                    new Event { Id = 2, Title = "Best Wedding Yet", Date = new DateTime(2018, 12, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "WED" }
                );

                builder.Entity<GuestBooking>().HasData(
                    new GuestBooking { CustomerId = 1, EventId = 1, Attended = true },
                    new GuestBooking { CustomerId = 2, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 1, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 3, EventId = 2, Attended = false }
                    );
                builder.Entity<Staff>().HasData(
                      new Staff { Id = 1, Surname = "Lee", FirstName = "Dickson", Email = "dickson@staff.example.com" },
                      new Staff { Id = 2, Surname = "Ang", FirstName = "Anson", Email = "anson@staff.example.com" },
                      new Staff { Id = 3, Surname = "Lim", FirstName = "Jamie", Email = "jamie@staff.example.com" }
                  );
             //   builder.Entity<StaffRole>().HasData(
             //    new Staff { Id = 1, Role = "Head of event" },
             //    new Staff { Id = 2, Role = "Event coordinator" },
             //    new Staff { Id = 3, Role = "Marketing lead" },
             //    new Staff { Id = 4, Role = "Sales/Customer Lead" },
             //    new Staff { Id = 5, Role = "On-Site Lead" },
             //    new Staff { Id = 6, Role = "Service Crew" }, 
             //    new Staff { Id = 7, Role = "First-aider" }
             //);
            }
        }


    }
}

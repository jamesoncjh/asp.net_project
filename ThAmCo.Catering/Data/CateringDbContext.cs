using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ThAmCo.Catering.Data
{
    public class CateringDbContext : DbContext
    {
        public DbSet<FoodBooking> FoodBooking { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<FoodList> FoodList { get; set; }

        private IWebHostEnvironment HostEnv { get; }
        public CateringDbContext(DbContextOptions<CateringDbContext> options,
                               IWebHostEnvironment env) : base(options)
        {
            HostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
            
        /*Controls the relationship here*/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("thamco.catering");

            builder.Entity<FoodBooking>()
                   .HasKey(f => new { f.FoodBookingId});

            builder.Entity<Menu>()
                   .HasMany(m => m.FoodBookingList)
                   .WithOne(f => f.Menu)
                   .HasForeignKey(f => f.MenuId);

            builder.Entity<FoodList>()
                    .HasKey(f => new { f.FoodListId});
            
            builder.Entity<Menu>()
                   .HasMany(m => m.FoodList)
                   .WithOne(f => f.Menu)
                   .HasForeignKey(f => f.MenuId);

            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                builder.Entity<FoodBooking>().HasData(
                    new FoodBooking { FoodBookingId = "1", Date = "06-06-2022", MenuId = "1" },
                    new FoodBooking { FoodBookingId = "2", Date = "07-06-2022", MenuId = "2" },
                    new FoodBooking { FoodBookingId = "3", Date = "08-06-2022", MenuId = "3" },
                    new FoodBooking { FoodBookingId = "4", Date = "09-06-2022", MenuId = "4" }
                );

                builder.Entity<Menu>().HasData(
                    new Menu { MenuId = "1", MenuName = "French Menu" },
                    new Menu { MenuId = "2", MenuName = "Canton Menu" },
                    new Menu { MenuId = "3", MenuName = "Italian Menu" },
                    new Menu { MenuId = "4", MenuName = "Malay Menu" }
                );

                builder.Entity<FoodList>().HasData(
                 new FoodList { FoodListId= 1 , FoodName = "Soupe à l’oignon", MenuId="1", Price=10.00},
                 new FoodList { FoodListId= 2 , FoodName = "Coq au vin", MenuId="1", Price = 20.00 },
                 new FoodList { FoodListId= 3 , FoodName = "Cassoulet", MenuId="1", Price = 30.00 },
                 new FoodList { FoodListId= 4 , FoodName = "Bœuf bourguignon", MenuId="1", Price = 40.00 },
                 new FoodList { FoodListId= 5 , FoodName = "Chocolate soufflé", MenuId="1", Price = 50.00 },
                 new FoodList { FoodListId= 6 , FoodName = "Steam Mihun with Ginger Wine Chicken", MenuId="2", Price = 60.00 },
                 new FoodList { FoodListId= 7 , FoodName = "Guang Xi Style Braised Pork with Man Tou ", MenuId="2", Price = 70.00},
                 new FoodList { FoodListId= 8 , FoodName = "Guang Xi Style Stuffed Taufu Ball ", MenuId="2", Price = 80.00 },
                 new FoodList { FoodListId= 9 , FoodName = "Claypot Kampung Chicken with Sesame Oil & Ginger " ,MenuId="2", Price = 90.00 },
                 new FoodList { FoodListId= 10 , FoodName = "Sweet and sour pork", MenuId="2", Price = 100.00 },
                 new FoodList { FoodListId= 11 , FoodName = "Lasagna Bolognese", MenuId="3", Price = 10.00 },
                 new FoodList { FoodListId= 12 , FoodName = "Veal Milanese", MenuId="3", Price = 20.00 },
                 new FoodList { FoodListId= 13 , FoodName = "Gnocchi Sorrento", MenuId="3", Price = 30.00 },
                 new FoodList { FoodListId= 14 , FoodName = "Spaghetti Carbonara", MenuId="3", Price = 40.00 },
                 new FoodList { FoodListId= 15 , FoodName = "Antipasto Italiano", MenuId="3", Price = 50.00 },
                 new FoodList { FoodListId= 16 , FoodName = "Ayam Percik", MenuId="4", Price = 60.00 },
                 new FoodList { FoodListId= 17 , FoodName = "Nasi Kerabu", MenuId="4", Price = 70.00 },
                 new FoodList { FoodListId= 18 , FoodName = "Nasi Lemak", MenuId="4", Price = 80.00 },
                 new FoodList { FoodListId= 19 , FoodName = "Beef Rendang", MenuId="4", Price = 90.00 },
                 new FoodList { FoodListId= 20 , FoodName = "Laksa", MenuId="4", Price = 100.00 }
                );
            }
        }
    }
}

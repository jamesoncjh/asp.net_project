using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThAmCo.Events.Data;
using Microsoft.Extensions.Hosting;

namespace ThAmCo.Events
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                //EVENTS
                var services = scope.ServiceProvider;
                var env = services.GetRequiredService<IWebHostEnvironment> ();
                
                //VENUES
                var venueServices = scope.ServiceProvider;
                var venuesEnv = services.GetRequiredService<IWebHostEnvironment>();   
                
                //CATERING
                var cateringServices = scope.ServiceProvider;
                var cateringEnv = services.GetRequiredService<IWebHostEnvironment>();

                if (env.IsDevelopment())
                {
                    //EVENTS
                    var context = services.GetRequiredService<EventsDbContext>();
                    context.Database.Migrate();

                    //VENUES
                    var venuesContext = venueServices.GetRequiredService<ThAmCo.Venues.Data.VenuesDbContext>();
                    venuesContext.Database.Migrate();

                    //CATERING
                    var cateringContext = cateringServices.GetRequiredService<ThAmCo.Catering.Data.CateringDbContext>();
                    cateringContext.Database.Migrate();
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

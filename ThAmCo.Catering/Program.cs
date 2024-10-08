﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThAmCo.Catering.Data;
using Microsoft.Extensions.Hosting;

namespace ThAmCo.Catering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetRequiredService<IWebHostEnvironment>();

                if (env.IsDevelopment())
                {
                    var context = services.GetRequiredService<CateringDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();

                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

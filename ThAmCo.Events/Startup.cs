using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThAmCo.Events.Data;

namespace ThAmCo.Events
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //EVENTS
            services.AddControllersWithViews();
            services.AddDbContext<EventsDbContext>(options =>
            {
                var cs = Configuration.GetConnectionString("EventsSqlConnection");
                options.UseSqlServer(cs);
            });
            //VENUES
            services.AddDbContext<ThAmCo.Venues.Data.VenuesDbContext>(options =>
            {
                var cs = Configuration.GetConnectionString("VenuesSqlConnection");
                options.UseSqlServer(cs);
            });
            //CATERING
            services.AddDbContext<ThAmCo.Catering.Data.CateringDbContext>(options =>
            {
                var cs = Configuration.GetConnectionString("CateringSqlConnection");
                options.UseSqlServer(cs);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}

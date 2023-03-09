using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission09_autdel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_autdel
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext> (options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            // Allow use of razor pages
            services.AddRazorPages();

            // Allow sessions
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // Corresponds to wwwroot folder
            app.UseSession(); // Use a session
            app.UseRouting();

            // Different endpoints in the correct order for routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("categorypage", "{category}/Page{pageNum}", new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("category", "category", new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapDefaultControllerRoute();

                // Used to allow razor pages
                endpoints.MapRazorPages();
            });
        }
    }
}

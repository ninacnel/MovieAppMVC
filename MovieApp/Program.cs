using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Services;

namespace MovieApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MovieDataContext>(
                o => o.UseNpgsql(builder.Configuration.GetConnectionString("MoviesDb")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //to add initial data if necessary
            app.Seed();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Other service registrations...
            services.AddScoped<MovieService>(); // Assuming MovieService has no dependencies
        }
    }
}
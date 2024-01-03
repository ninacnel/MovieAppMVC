using MovieApp.Data;
using MovieApp.Data.Models;

namespace MovieApp
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<MovieDataContext>();
            context.Database.EnsureCreated();
            AddMovies(context);
        }

        private static void AddMovies(MovieDataContext context)
        {
            var movie = context.Movies.FirstOrDefault();
            if (movie != null) return;

            context.Movies.Add(new Movie
            {
                Title = "Esperando la Carroza",
                Year = 1985,
                Summary = "An out of ordinary weekend in the life of an argentine family.",
                Actors = new List<Actor>
                {
                    new Actor { Fullname = "Luis Brandoni"},
                    new Actor { Fullname = "China Zorrilla"}
                }
            });

            context.Movies.Add(new Movie
            {
                Title = "Emma",
                Year = 2020,
                Summary = "Handsome, Clever, and Rich.",
                Actors = new List<Actor>
                {
                    new Actor { Fullname = "Anya Taylor Joy"},
                    new Actor { Fullname = "Mia Goth"}
                }
            });

            context.SaveChanges();
        }
    }
}

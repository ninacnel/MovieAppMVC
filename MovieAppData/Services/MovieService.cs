using Microsoft.EntityFrameworkCore;
using MovieApp.ViewModels;
using MovieAppData;

namespace MovieApp.Data.Services
{
    public class MovieService
    {
        private readonly MovieDataContext _context;

        public MovieService(MovieDataContext context)
        {
            _context = context;
        }

        public List<MovieViewModel> GetMovies()
        {
            var movies = _context.Movies.Include(m => m.Actors).Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Summary = m.Summary,
                Actors = m.Actors.Select(a => a.Fullname).ToList()
            }).ToList();
            return movies;
        }

        public MovieViewModel GetMovie(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Actors) // Explicitly include Actors
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                // Handle the case where no movie with the specified ID is found
                return null;
            }

            var movieViewModel = new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Summary = movie.Summary,
                Actors = movie.Actors?.Select(a => a.Fullname).ToList() ?? new List<string>()
            };

            return movieViewModel;
        }


        public MovieViewModel AddMovie(MovieViewModel movie)
        {
            // Create a new Movie entity
            var newMovie = new Movie
            {
                Title = movie.Title,
                Year = movie.Year,
                Summary = movie.Summary,
                Actors = movie.Actors.Select(actor => new Actor { Fullname = actor }).ToList()
            };

            // Add the new movie to the context
            _context.Movies.Add(newMovie);

            // Save changes to the database
            _context.SaveChanges();

            // Map the new movie back to a view model and return it
            var newMovieViewModel = new MovieViewModel
            {
                Title = newMovie.Title,
                Year = newMovie.Year,
                Summary = newMovie.Summary,
                Actors = newMovie.Actors.Select(a => a.Fullname).ToList()
            };

            return newMovieViewModel;
        }
    }
}

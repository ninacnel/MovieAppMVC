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
                State = m.State,
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
                State = movie.State,
                Actors = movie.Actors?.Select(a => a.Fullname).ToList() ?? new List<string>()
            };

            return movieViewModel;
        }


        public MovieViewModel AddMovie(MovieViewModel movie)
        {
            var newMovie = new Movie
            {
                Title = movie.Title,
                Year = movie.Year,
                Summary = movie.Summary,
                State = true,
                Actors = movie.Actors.Select(actor => new Actor { Fullname = actor }).ToList()
            };

            _context.Movies.Add(newMovie);

            _context.SaveChanges();

            var newMovieViewModel = new MovieViewModel
            {
                Title = newMovie.Title,
                Year = newMovie.Year,
                Summary = newMovie.Summary,
                State = newMovie.State,
                Actors = newMovie.Actors.Select(a => a.Fullname).ToList()
            };

            return newMovieViewModel;
        }

		public MovieViewModel ModifyMovie(MovieViewModel movie)
		{
			var movieDB = _context.Movies
				.Include(m => m.Actors)
				.SingleOrDefault(m => m.Id == movie.Id);

			if (movieDB != null)
			{
				// Update the properties of the existing movieDB
				movieDB.Title = movie.Title;
				movieDB.Year = movie.Year;
				movieDB.Summary = movie.Summary;

				// Clear existing actors and add new actors
				movieDB.Actors.Clear();
				movieDB.Actors.AddRange(movie.Actors.Select(actor => new Actor { Fullname = actor }));

				_context.SaveChanges();

				var updatedMovie = new MovieViewModel
				{
					Title = movieDB.Title,
					Year = movieDB.Year,
					Summary = movieDB.Summary,
					Actors = movieDB.Actors.Select(a => a.Fullname).ToList()
				};

				return updatedMovie;
			}

			// Handle the case where no movie with the specified ID is found
			return null;
		}

        public void SoftDeleteMovie(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);
            if (movie != null)
            {
                movie.State = false;
                _context.SaveChanges();
            }
        }

        public void RecoverMovie(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);
            if (movie != null)
            {
                movie.State = true;
                _context.SaveChanges();
            }
        }
    }
}

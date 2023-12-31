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
                Title = m.Title,
                Year = m.Year,
                Summary = m.Summary,
                Actors = string.Join(',', m.Actors.Select(a => a.Fullname))
            }).ToList();
            return movies;
        }
    }
}

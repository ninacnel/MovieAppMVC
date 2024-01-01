using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Services;
using MovieApp.Models;
using MovieApp.ViewModels;
using System.Diagnostics;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieService _service;

        public HomeController(MovieService service)
        {
            _service = service; 
        }

        public ActionResult Index()
        {
            var movies = _service.GetMovies();
            return View(movies);
        }

        [ActionName("Movie")]
        public ActionResult Index(int id)
        {
            var movie = _service.GetMovie(id);
            return View(movie);
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                _service.AddMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Render the initial view for modifying a movie
        [HttpGet]
        public IActionResult ModifyMovie(int id)
        {
            MovieViewModel movie = _service.GetMovie(id);
            return View(movie);
        }

        // POST: Handle the form submission for modifying a movie
        [HttpPost]
        public IActionResult ModifyMovie(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                _service.ModifyMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
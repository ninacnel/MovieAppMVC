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
        private readonly MovieDataContext _context;
        private readonly MovieService _service;

        public HomeController(MovieDataContext context, MovieService service)
        {
            _context = context;
            _service = service; 
        }

        public ActionResult Index()
        {
            var movies = _service.GetMovies();
            return View(movies);
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
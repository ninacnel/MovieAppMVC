using MovieAppData;

namespace MovieApp.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
        public List<string> Actors { get; set; }
    }
}


using MovieAppData;

namespace MovieApp.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
        public bool State { get; set; }
        public List<string> Actors { get; set; }
    }
}


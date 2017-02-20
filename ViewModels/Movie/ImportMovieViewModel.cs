using FilmFind.Entities.Models;

namespace FilmFind.ViewModels.Movie
{
    public class ImportMovieViewModel
    {
        public MovieSummary Movie { get; set; }
        public bool IsInDb { get; set; }
    }
}

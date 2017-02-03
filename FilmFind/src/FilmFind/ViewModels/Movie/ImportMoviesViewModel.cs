using System.Collections.Generic;

namespace FilmFind.ViewModels.Movie
{
    public class ImportMoviesViewModel
    {
        public List<ImportMovieViewModel> ImportMovies { get; set; }
        public string Message { get; set; }

        public ImportMoviesViewModel()
        {
            ImportMovies = new List<ImportMovieViewModel>();
        }
    }
}

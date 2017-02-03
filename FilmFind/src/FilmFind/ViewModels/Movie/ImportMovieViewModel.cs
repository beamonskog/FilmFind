using System.Collections.Generic;

namespace FilmFind.ViewModels.Movie
{
    public class ImportMovieViewModel
    {
        public IEnumerable<Entities.Movie> Movies { get; set; }
        public string Message { get; set; }
    }
}

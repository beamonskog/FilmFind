using FilmFind.ViewModels.Movie;
using System.Collections.Generic;

namespace FilmFind.ViewModels
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {

        }

        public IEnumerable<SmallPreviewViewModel> LatestMovies { get; set; }
        public IEnumerable<SmallPreviewViewModel> TopRatedMovies { get; set; }
        public IEnumerable<SmallPreviewViewModel> HypedMovies { get; set; }
    }
}

using FilmFind.Entities;

namespace FilmFind.ViewModels.Movie
{
    public class ImportMovieViewModel
    {
        public Entities.Movie Movie { get; set; }
        public bool IsInDb { get; set; }
    }

    //public class ImportMovieSummaryViewModel
    //{
    //    public MovieSummary Movie { get; set; }
    //    public bool IsInDb { get; set; }
    //}

}

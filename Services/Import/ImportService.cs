using FilmFind.Entities.Models;
using System.Collections.Generic;

namespace FilmFind.Services.Import
{
    public interface IimportService
    {
        List<MovieSummary> GetMovieSummaries(string searchString, int count);
        Movie GetCompleteMovie(string title);
        List<IMovie> GetCompleteMovieList(string searchString, int count);

    }
}

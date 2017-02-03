using FilmFind.Entities;
using FilmFind.Services;
using FilmFind.ViewModels;
using FilmFind.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmFind.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieData _allMovies;

        public HomeController(IMovieData allMovies)
        {
            _allMovies = allMovies;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();

            var latest = _allMovies.GetLatest(5);
            var topRated = _allMovies.GetTopRated(5);
            var hyped = _allMovies.GetMostHyped(5);
            if (latest != null)
            {
                model.LatestMovies = latest.Select(m => ConvertToSmallPreview(m));
                model.TopRatedMovies = topRated.Select(m => ConvertToSmallPreview(m));
                model.HypedMovies = hyped.Select(m => ConvertToSmallPreview(m));
            }

            return View(model);
        }

        public IActionResult AboutMovie(int id)
        {
            var movie = _allMovies.Get(id);
            return View(movie);
        }

        private SmallPreviewViewModel ConvertToSmallPreview(Movie inputMovie)
        {
            return new SmallPreviewViewModel
            {
                Id = inputMovie.Id,
                PosterUrl = inputMovie.PosterUrl,
                Title = inputMovie.Title
            };
        }
    }
}
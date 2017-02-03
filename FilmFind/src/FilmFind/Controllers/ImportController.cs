using Microsoft.AspNetCore.Mvc;
using FilmFind.Entities;
using FilmFind.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using FilmFind.ViewModels.Movie;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmFind.Controllers
{
    [Authorize]
    public class ImportController : Controller
    {
        private readonly IMovieData _sqlMovieData;

        //private Movie _newImportMovie { get; set; }

        public ImportController(IMovieData movieData)
        {
            _sqlMovieData = movieData;
        }

        public IActionResult Index(IEnumerable<Movie> movies)
        {
            if (movies == null)
            {
                return View(movies);

            }
            return View();
        }

        /// <summary>
        /// Uses OMDB to try and import the desired movie.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Import(SearchData searchTerm)
        {
            var helper = new ImportService();
            var movies = helper.GetMoviesOnline(searchTerm.SearchText).Result;
            if (movies != null)
            {
                ImportMoviesViewModel model = GetImportModelFromMovies(movies);
                return View("Index", model);
            }
            else
            {
                return View();
            }
        }

        private ImportMoviesViewModel GetImportModelFromMovies(List<Movie> movies)
        {
            if (movies.Count == 0)
            {
                return null;
            }

            var resultModel = new ImportMoviesViewModel();
            foreach (var movie in movies)
            {
                var isInDb = _sqlMovieData.Get(movie.Title) != null;
                var thisModel = new ImportMovieViewModel()
                {
                    Movie = movie,
                    IsInDb = isInDb
                };
                resultModel.ImportMovies.Add(thisModel);
            }
            return resultModel;
        }

        [HttpPost]
        public void AddToDb(ImportMovieViewModel newMovie)
        {
            if (_sqlMovieData.GetAll().Any(m => m.Title == newMovie.Movie.Title))
            {
                //return Content($"\"{newMovie.Title}\" is already in db!");
                throw new Exception($"\"{newMovie.Movie.Title}\" is already in db!");
            }
            _sqlMovieData.Add(newMovie.Movie);
            //return View("Import");
            //return View("index");
            //return RedirectToAction(actionName: "index", controllerName: "home");
        }

    }
}

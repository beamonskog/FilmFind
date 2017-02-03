using Microsoft.AspNetCore.Mvc;
using FilmFind.ViewModels;
using FilmFind.Entities;
using FilmFind.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
            if (movies==null)
            {
                return View(movies);

            }
            return View();
        }

        /// <summary>
        /// Uses OMDB to try and import the sought movie.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Import(SearchData searchTerm)
        {
            var helper = new SearchService();
            var movies = helper.GetMoviesOnline(searchTerm.SearchText).Result;
            if (movies != null)
            {
                return View("Index",movies);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddToDb(Movie newMovie)
        {
            if (_sqlMovieData.GetAll().Any(m => m.Title == newMovie.Title))
            {
                return Content($"\"{newMovie.Title}\" is already in db!");

            }
            _sqlMovieData.Add(newMovie);
            return View("index");
            //return RedirectToAction(actionName: "index", controllerName: "home");
        }

    }
}

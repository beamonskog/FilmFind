using Microsoft.AspNetCore.Mvc;
using FilmFind.Entities;
using FilmFind.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using FilmFind.ViewModels.Movie;
using FilmFind.Services.Import;
using AutoMapper;
using FilmFind.Entities.Models;
using FilmFind.ViewModels.UserMovie;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmFind.Controllers
{
    [Authorize]
    public class ImportController : Controller
    {
        private readonly IMovieData _sqlMovieData;
        private readonly IMapper _mapper;

        public ImportController(IMovieData movieData, IMapper mapper)
        {
            _sqlMovieData = movieData;
            _mapper = mapper;
        }

        public IActionResult Index(IEnumerable<IMovie> movies)
        {
            //_sqlMovieData.UpdateCurrentMovies(_mapper);

            if (movies == null)
            {
                return View(movies);

            }
            return View();
        }

        [HttpGet]
        public IActionResult Import(SearchData searchTerm)
        {
            var helper = new OMDBImportService(_mapper);
            var movies = helper.GetMovieSummaries(searchTerm.SearchText, 15);
            //var movies = helper.GetCompleteMovieList(searchTerm.SearchText, 15);
            if (movies != null)
            {
                var model = MarkExistingMovies(movies);
                return View("Index", model);
            }
            else
            {
                return View();
            }
        }

        private ImportMoviesViewModel MarkExistingMovies(List<MovieSummary> movies)
        {
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
        public IActionResult AddToDb(MovieTitle importMovie)
        {
            if (_sqlMovieData.Find(importMovie.Title) != null)
            {
                throw new Exception($"\"{importMovie.Title}\" is already in db!");
            }

            var helper = new OMDBImportService(_mapper);
            var theMovie = helper.GetCompleteMovie(importMovie.Title);

            _sqlMovieData.Add(theMovie);
            return View("index");
        }
    }
}

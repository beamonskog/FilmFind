﻿using FilmFind.Entities;
using FilmFind.Services;
using FilmFind.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FilmFind.Controllers
{
    public class SearchController:Controller
    {
        private IMovieData _allMovies;
        public SearchController(IMovieData allMovies)
        {
            _allMovies = allMovies;
        }

        //[HttpGet]
        public IActionResult Index(SearchData searchString)
        {
            var searchResults = _allMovies.Search(searchString.SearchText);
            var models = new List<SmallMovieSummaryViewModel>();
            foreach (var searchResult in searchResults)
            {
                models.Add(new SmallMovieSummaryViewModel()
                {
                    Director = searchResult.Director,
                    Genres = searchResult.Genre,
                    MovieId = searchResult.Id,
                    Title = searchResult.Title,
                    UserAverage = searchResult.AverageUserRating,
                    PosterUrl = searchResult.PosterUrl,
                    ReleaseYear = searchResult.Year
                });
            }

            return View(models);
        }
    }
}

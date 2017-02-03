using FilmFind.Entities;
using FilmFind.Services;
using FilmFind.ViewModels.Movie;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FilmFind.Controllers
{
    /// <summary>
    /// Displays movies for users that both are and aren't logged in
    /// </summary>
    public class MovieController : Controller
    {
        private IMovieData _allMovies;
        private UserManager<User> _userManager;
        private IUserMovieDataService _userMovieData;

        public MovieController(UserManager<User> userManager, IUserMovieDataService userMovieData, IMovieData allMovies)
        {
            _userManager = userManager;
            _userMovieData = userMovieData;
            _allMovies = allMovies;
        }

        public IActionResult About(int id)
        {
            var movie = _allMovies.GetComplete(id);
            var model = new MovieViewModel
            {
                AverageUserRating = movie.AverageUserRating,
                Director = movie.Director,
                MovieId = id,
                Genre = movie.Genre,
                IMDBRating = movie.IMDBRating,
                PosterUrl = movie.PosterUrl,
                RottenTomatoesRating = movie.RottenTomatoesRating,
                Title = movie.Title,
                Year = movie.Year,
                UserOpinion = new List<UserOpinionViewModel>()
            };

            var relevantUserMovies = movie?.AddedByUsers?.Where(u => !string.IsNullOrEmpty(u.UserMovies.First().UserComment))
                .Select(x => new
                {
                    UserMovie = x.UserMovies.First(),
                    UserName = x.UserName
                });

            //What happens if relevantUserMovies is empty?
            foreach (var userMovie in relevantUserMovies)
            {
                model.UserOpinion.Add(
                    new UserOpinionViewModel()
                    {
                        Rating = userMovie.UserMovie.UserRating,
                        Added = userMovie.UserMovie.Added,
                        Comment = userMovie.UserMovie.UserComment,
                        UserName = userMovie.UserName
                    });
            }

            //Logged in
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            if (currentUserId != null)
            {
                var userMovie = _userMovieData.Get(currentUserId, id);
                if (userMovie != null)//IsInList
                {
                    model.IsFavorite = userMovie.Favored;
                    model.IsHyped = userMovie.Hyped;
                    model.IsInList = true;
                }
            }

            return View(model);
        }


    }
}

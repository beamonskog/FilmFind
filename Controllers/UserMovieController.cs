using FilmFind.Entities;
using FilmFind.Entities.Models;
using FilmFind.Services;
using FilmFind.ViewModels.Movie;
using FilmFind.ViewModels.UserMovie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmFind.Controllers
{

    [Authorize]
    public class UserMovieController : Controller
    {
        private UserManager<User> _userManager;
        private readonly IMovieData _sqlMovieData;
        private IUserMovieDataService _userMovieData;

        public UserMovieController(UserManager<User> userManager, IMovieData sqlMovieData, IUserMovieDataService userMovieData)
        {
            _userManager = userManager;
            _sqlMovieData = sqlMovieData;
            _userMovieData = userMovieData;
        }

        public IActionResult Index()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);// FindByIdAsync(userId); 
            var userMovies = _userMovieData.GetAllUserMovies(currentUserId);
            if (userMovies == null)
            {
                return View(new List<UserMovieViewModel>());
            }
            var movies = new List<Movie>();

            //POPULATE MODELS
            var models = new List<UserMovieViewModel>();

            foreach (var userMovie in userMovies)
            {
                models.Add(new UserMovieViewModel()
                {
                    Movie = _sqlMovieData.Get(userMovie.MovieId),
                    Comment = userMovie.UserComment,
                    Rating = userMovie.UserRating,
                    IsFavorite = userMovie.Favored,
                    IsHyped = userMovie.Hyped,
                    Added = userMovie.Added
                });
            }

            return View(models);
        }

        [HttpGet]
        public IActionResult EditUserMovie(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);// FindByIdAsync(userId); 
            var userMovie = _userMovieData.Get(currentUserId, id);

            var viewModel = new UserMovieViewModel()
            {
                Movie = _sqlMovieData.Get(userMovie.MovieId),
                Comment = userMovie.UserComment,
                Rating = userMovie.UserRating,
                IsFavorite = userMovie.Favored,
                IsHyped = userMovie.Hyped,
                Added = userMovie.Added
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUserMovie(int id, EditViewModel model)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var userMovie = _userMovieData.Get(currentUserId, id);
            var oldRating = userMovie.UserRating;
            if (ModelState.IsValid)
            {
                userMovie.UserComment = model.Comment;
                userMovie.UserRating = model.Rating;

                if (oldRating != userMovie.UserRating)
                {
                    var movie = _sqlMovieData.GetComplete(id);
                    _sqlMovieData.SetUserAverage(movie);
                }

                _userMovieData.Commit();
                return RedirectToAction("index");
            }
            return View();
        }

        public IActionResult AddUserMovie(int id)
        {
            var movie = _sqlMovieData.Get(id);
            var model = new MovieViewModel
            {
                AverageUserRating = movie.AverageUserRating,
                Director = movie.Director,
                MovieId = id,
                Genres = movie.Genres.Select(g=>g.Genre).ToList(),
                IMDBRating = movie.IMDBRating,
                PosterUrl = movie.Poster,
                RottenTomatoesRating = movie.RottenTomatoesRating,
                Title = movie.Title,
                Year = movie.Year
            };
            return View("AddUserMovie", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserMovie(AddViewModel userMovieViewModel)
        {
            //Add to UserMoviesList
            if (ModelState.IsValid)
            {
                var currentUserId = _userManager.GetUserId(HttpContext.User);
                var movie = _sqlMovieData.GetComplete(userMovieViewModel.MovieId);

                var movieToAdd = new UserMovie()
                {
                    Added = DateTime.UtcNow,
                    MovieId = userMovieViewModel.MovieId,
                    UserComment = userMovieViewModel.Comment,
                    UserRating = userMovieViewModel.Rating
                };

                var resultMovie = await _userMovieData.Add(currentUserId, movieToAdd);
                movie = _sqlMovieData.AddUser(movie, currentUserId);

                //Add rating to AllMoviesList
                if (resultMovie.UserRating != 0 )
                {
                    _sqlMovieData.SetUserAverage(movie);
                }

                _sqlMovieData.Commit();
                return RedirectToAction(controllerName: "movie", actionName: "about", routeValues: new { id = movie.Id });
            }
            return View();
        }

        public async Task<IActionResult> RemoveUserMovie(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var userMovie = _userMovieData.Get(currentUserId, id);
            await _userMovieData.Remove(currentUserId, userMovie);

            var movie = _sqlMovieData.GetComplete(id);
            movie = _sqlMovieData.RemoveUser(movie, currentUserId);
            _sqlMovieData.SetUserAverage(movie);
            _sqlMovieData.Commit();
            return RedirectToAction(controllerName: "movie", actionName: "about", routeValues: new { id });//View("aboutMovie",id);
        }

        public IActionResult ToggleFavorite(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var userMovie = _userMovieData.Get(currentUserId, id);

            var isFavorite = userMovie.Favored;
            var toggleOkay = isFavorite ? RemoveFavorite(userMovie) : AddFavorite(userMovie);

            return RedirectToAction("index");
        }

        public async Task<IActionResult> ToggleAddRemove(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var userMovie = _userMovieData.Get(currentUserId, id);
            if (userMovie == null)
            {
                return AddUserMovie(id);
            }

            else
            {
                return await RemoveUserMovie(id);
            }
            //return RedirectToAction(controllerName: "Movie", actionName: "about", routeValues: new { id });
        }

        public IActionResult ToggleHype(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var userMovie = _userMovieData.Get(currentUserId, id);

            var isHyped = userMovie.Hyped;
            var toggleOkay = isHyped ? RemoveHype(userMovie) : AddHype(userMovie);

            return RedirectToAction("index");
        }

        private bool AddFavorite(UserMovie userMovie)
        {
            userMovie = _userMovieData.AddFavorite(userMovie);
            _userMovieData.Commit();

            var movie = _sqlMovieData.Get(userMovie.MovieId);
            _sqlMovieData.AddFavorite(movie);
            _sqlMovieData.Commit();
            return true;
        }

        private bool AddHype(UserMovie userMovie)
        {
            userMovie = _userMovieData.AddHype(userMovie);
            _userMovieData.Commit();

            var movie = _sqlMovieData.Get(userMovie.MovieId);
            _sqlMovieData.AddHype(movie);
            _sqlMovieData.Commit();

            return true;
        }

        private bool RemoveFavorite(UserMovie userMovie)
        {
            userMovie = _userMovieData.RemoveFavorite(userMovie);
            _userMovieData.Commit();

            var movie = _sqlMovieData.Get(userMovie.MovieId);
            _sqlMovieData.RemoveFavorite(movie);
            _sqlMovieData.Commit();
            return true;
        }

        private bool RemoveHype(UserMovie userMovie)
        {
            userMovie = _userMovieData.RemoveHype(userMovie);
            _userMovieData.Commit();

            var movie = _sqlMovieData.Get(userMovie.MovieId);
            _sqlMovieData.RemoveHype(movie);
            _sqlMovieData.Commit();
            return true;
        }
    }
}

using FilmFind.Entities;
using FilmFind.Services;
using FilmFind.ViewModels.UserMovie;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmFind.ViewComponents
{
    public class ToggleMovieIconsViewComponent : ViewComponent
    {
        private UserManager<User> _userManager;
        private IUserMovieDataService _userMovieData;

        public ToggleMovieIconsViewComponent(IUserMovieDataService userMovieData, UserManager<User> userManager)
        {
            _userMovieData = userMovieData;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(bool isHyped, bool isFavored, int movieId, bool isAdded)
        {
            var viewModel = new ToggleIconsViewModel()
            {
                IsFavorite = isFavored,
                IsHyped = isHyped,
                MovieId = movieId,
                IsAdded = isAdded
            };
            return View(viewModel);
        }

    }
}

using FilmFind.Entities;
using FilmFind.Services;
using FilmFind.ViewModels.UserMovie;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmFind.ViewComponents
{
    public class ToggleInListViewComponent : ViewComponent
    {
        private UserManager<User> _userManager;
        private IUserMovieDataService _userMovieData;

        public ToggleInListViewComponent(IUserMovieDataService userMovieData, UserManager<User> userManager)
        {
            _userMovieData = userMovieData;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(bool isHyped, bool isFavored, int movieId)
        {
            var viewModel = new ToggleIconsViewModel()
            {
                IsFavorite = isFavored,
                IsHyped = isHyped,
                MovieId = movieId
            };
            return View(viewModel);
        }
    }
}

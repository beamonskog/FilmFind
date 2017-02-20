using FilmFind.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmFind_2.Controllers
{
    public class AdminController:Controller
    {
        private UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() { return View(); }
    }
}

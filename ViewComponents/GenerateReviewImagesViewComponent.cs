using FilmFind.Entities.Models;
using FilmFind.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;

namespace FilmFind.ViewComponents
{
     public class GenerateReviewImagesViewComponent: ViewComponent
    {
       public  IViewComponentResult Invoke(double userAverage, TomatoeImage tomatoeImage, string tomatoeScore, string imdbScore)
        {
            var model = new GenerateIconsViewModel
            {
                ImdbScore = imdbScore,
                TomatoeImage = tomatoeImage,
                TomatoeScore = tomatoeScore,
                UserAverage = userAverage

            };
            return View(model);
        }

    }
}

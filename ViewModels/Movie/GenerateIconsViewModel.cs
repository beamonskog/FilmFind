using FilmFind.Entities.Models;

namespace FilmFind.ViewModels.Movie
{
    public class GenerateIconsViewModel
    {
        public double UserAverage { get; set; }
        public TomatoeImage TomatoeImage { get; set; }
        public string TomatoeScore { get; set; }
        public string ImdbScore { get; set; }
    }
}

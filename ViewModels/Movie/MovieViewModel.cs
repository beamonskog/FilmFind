using FilmFind.Entities;
using FilmFind.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmFind.ViewModels.Movie
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }

        public string Title { get; set; }
        public List<Genre> Genres { get; set; }
        public string Director { get; set; }
        public string Year { get; set; }
        public string Summary { get; set; }

        public string IMDBRating { get; set; }
        public string RottenTomatoesRating { get; set; }
        public TomatoeImage TomatoeImage { get; set; }
        public string PosterUrl { get; set; }

        [Display(Name = "User average")]
        public double AverageUserRating { get; set; }

        public bool IsInList { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsHyped { get; set; }

        public ICollection<UserOpinionViewModel> UserOpinion { get; set; }
    }
}

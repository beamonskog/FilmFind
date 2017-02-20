using System;
using FilmFind.Entities;

namespace FilmFind.ViewModels.UserMovie
{
    public class UserMovieViewModel
    {
        public Entities.Models.Movie Movie { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsHyped { get; set; }
        public DateTime Added {get;set;}
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmFind.Entities
{
    //TODO: annotations
    public class Movie
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Plot { get; set; }
        public string Year { get; set; }

        public string IMDBRating { get; set; }
        public string RottenTomatoesRating { get; set; }
        public string PosterUrl { get; set; }

        [Display(Name = "User average")]
        public double AverageUserRating { get; set; }
        public int Hyped { get; set; }
        public int Favorited { get; set; }
        public ICollection<User> AddedByUsers { get; set; }
        //public ICollection<string> AllComments { get; set; }
        //public ICollection<UserMovieRating> UserRatings { get; set; }

    }

   //public enum Genre
   // {
   //     Drama,
   //     Crime,
   //     Thriller,
   //     Action,
   //     Adventure,
   //     SciFi,
   //     Horror,
   //     Comedy,
   //     Fantasy,
   //     Biography,
   //     Sport,
   //     Family
   // }
}

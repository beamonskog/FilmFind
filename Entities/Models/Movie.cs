using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmFind.Entities.Models
{
    public interface IMovie
    {
        string Title { get; set; }
        string Year { get; set; }
        string imdbID { get; set; }
        string Type { get; set; }
        string Poster { get; set; }
    }

    //TODO: annotations
    public class Movie : IMovie
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public string Title { get; set; }
        public List<GenreData> Genres { get; set; }
        public string Director { get; set; }
        public string Plot { get; set; }
        public string Year { get; set; }

        public string IMDBRating { get; set; }
        public string RottenTomatoesRating { get; set; }
        public TomatoeImage TomatoeImage { get; set; }

        public string Poster { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }

        [Display(Name = "User average")]
        public double AverageUserRating { get; set; }
        public int Hyped { get; set; }
        public int Favorited { get; set; }
        public ICollection<User> AddedByUsers { get; set; }

        public Movie()
        {
            Created = DateTime.Now;
        }
    }

    public class MovieSummary : IMovie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }

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

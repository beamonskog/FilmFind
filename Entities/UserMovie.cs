using System;
using System.ComponentModel.DataAnnotations;

namespace FilmFind.Entities
{
    //TODO: annotations
    public class UserMovie
    {
        public int MovieId { get; set; }

        public int Id { get; set; }
        public DateTime Added { get; set; }

        [Range(0, 5)]
        [Display(Name = "My Rating")]
        public int UserRating { get; set; }
        [MaxLength(2048)]
        [Display(Name = "My Comment")]
        public string UserComment{ get; set; }
        public bool Favored { get; set; }
        public bool Hyped { get; set; }

    }
}
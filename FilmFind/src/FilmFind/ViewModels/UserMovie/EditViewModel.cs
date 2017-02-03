using System.ComponentModel.DataAnnotations;

namespace FilmFind.ViewModels.UserMovie
{
    //TODO: Identical to AddUserMovieViewModel
    public class EditViewModel
    {
        public int MovieId { get; set; }

        [MaxLength(2048)]
        public string Comment { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
    }
}

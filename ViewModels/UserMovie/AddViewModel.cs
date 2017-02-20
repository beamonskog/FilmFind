using System.ComponentModel.DataAnnotations;

namespace FilmFind.ViewModels.UserMovie
{
    public class AddViewModel
    {
        public int MovieId { get; set; }
        [ MaxLength(2048)]
        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}

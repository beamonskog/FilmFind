using System;

namespace FilmFind.ViewModels.Movie
{
    public class UserOpinionViewModel
    {
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Added { get; set; }
    }
}

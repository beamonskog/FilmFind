﻿using FilmFind.Entities;
using System.Collections.Generic;

namespace FilmFind.ViewModels.Movie
{
    public class SmallMovieSummaryViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public double UserAverage { get; set; }
        public List<Genre> Genres { get; set; }
        public string PosterUrl { get; set; }
        public string ReleaseYear { get; set; }
    }
}

using AutoMapper;
using FilmFind.Entities;
using FilmFind.Entities.Models;
using FilmFind.Services.Import;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmFind.Services
{
    public interface IMovieData
    {
        void UpdateCurrentMovies(IMapper mapper);
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> GetLatest(int count);
        IEnumerable<Movie> GetTopRated(int count);
        IEnumerable<Movie> GetMostHyped(int count);
        IEnumerable<Movie> Search(string searchString);
        Movie GetShort(int id);
        Movie Get(string title);
        Movie GetComplete(int id);
        Movie Find(string title);
        Movie Add(Movie newMovie);
        Movie AddHype(Movie movie);
        Movie AddFavorite(Movie movie);
        Movie RemoveFavorite(Movie movie);
        Movie RemoveHype(Movie movie);
        Movie SetUserAverage(Movie movie);
        Movie AddUser(Movie movie, string currentUserId);
        Movie RemoveUser(Movie movie, string currentUserId);
        void Remove(Movie movie);
        void Commit();
    }

    public class SqlMovieData : IMovieData
    {
        private FilmFindDbContext _context;

        public SqlMovieData(FilmFindDbContext context, IUserMovieDataService userMovieDataService)
        {
            _context = context;
        }

        public Movie Find(string title)
        {
            return _context.Movies.Where(m => m.Title == title).FirstOrDefault();
        }

        public Movie Add(Movie newMovie)
        {
            _context.Add(newMovie);
            _context.SaveChanges();
            return newMovie;
        }

        public void Remove(Movie movie)
        {
            _context.Movies.Remove(movie);
            //_context.SaveChanges();
        }

        public Movie AddFavorite(Movie movie)
        {
            movie.Favorited++;
            _context.Update(movie);
            return movie;
        }

        public Movie AddHype(Movie movie)
        {
            movie.Hyped++;
            _context.Update(movie);
            return movie;
        }

        public void UpdateComment(UserMovie userMovie)
        {
            AddComment(userMovie);
        }

        private void AddComment(UserMovie userMovie)
        {
            throw new NotImplementedException();
        }

        public Movie GetShort(int id)
        {
            var movie = _context.Movies.
                Where(m => m.Id == id).
                FirstOrDefault();

            return movie;
        }

        public Movie Get(string title)
        {
            var movie = _context.Movies.
                Where(m => m.Title == title).Include(m=>m.Genres).
                FirstOrDefault();

            return movie;
        }

        public Movie GetComplete(int id)
        {
            var userMovies = _context.UserMovies.Where(um => um.MovieId == id).ToList();//future!
            var movie = _context.Movies.
                Where(m => m.Id == id).
                Include("AddedByUsers").
                Include("Genres").
                FirstOrDefault();

            return movie;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies;
        }

        public IEnumerable<Movie> GetLatest(int count)
        {
            var sortedList = _context.Movies.OrderByDescending(m => m.Created).ToList();
            return GetRange(sortedList, count);
        }

        public IEnumerable<Movie> GetMostHyped(int count)
        {
            var sortedList = _context.Movies.OrderByDescending(m => m.Hyped).ToList();
            return GetRange(sortedList, count);
        }

        public IEnumerable<Movie> GetTopRated(int count)
        {
            var sortedList = _context.Movies.OrderByDescending(m => m.AverageUserRating).ToList();
            return GetRange(sortedList, count);
        }

        private IEnumerable<Movie> GetRange(List<Movie> movieList, int desiredNumber)
        {
            if (movieList.Count >= desiredNumber)
            {
                return movieList.GetRange(0, desiredNumber);
            }
            else if (movieList.Count < desiredNumber)
            {
                return movieList;
            }
            else
            {
                return null;
            }
        }

        public Movie RemoveFavorite(Movie movie)
        {
            movie.Favorited--;
            //_context.Update(movie);
            return movie;
        }

        public Movie RemoveHype(Movie movie)
        {
            movie.Hyped--;
            //_context.Update(movie);
            return movie;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Movie SetUserAverage(Movie movie)
        {
            var addedByUsers = movie.AddedByUsers;
            double averageRating = 0;

            if (addedByUsers.Count != 0)
            {
                var userMovies = addedByUsers.Select(u => u.UserMovies.Where(um => um.MovieId == movie.Id).First()).ToList();
                averageRating = userMovies.
                    Where(um => um.UserRating != 0).
                    Select(um => um.UserRating).
                    Average();
            }

            movie.AverageUserRating = averageRating;
            return movie;
        }

        public Movie AddUser(Movie movie, string userId)
        {
            var user = GetCurrentUser(userId);
            if (movie.AddedByUsers.Contains(user))
            {
                throw new Exception("User is already in list!");
            }

            movie.AddedByUsers.Add(user);
            return movie;
        }

        private User GetCurrentUser(string userId)
        {
            var currentUser = _context.Users.
            Include(c => c.UserMovies).
            Where(m => m.Id == userId).FirstOrDefault();

            return currentUser;
        }

        public Movie RemoveUser(Movie movie, string userId)
        {
            var user = GetCurrentUser(userId);
            movie.AddedByUsers.Remove(user);
            return movie;
            //_context.Update(movie);
        }

        public IEnumerable<Movie> Search(string searchString)
        {
            return _context.Movies.Where(s => s.Title.Contains(searchString)).ToList();
            //throw new NotImplementedException();
        }



        public void UpdateCurrentMovies(IMapper mapper)
        {
            var importer = new OMDBImportService(mapper);

            var allMovies = GetAll();
            for (int i = 0; i < allMovies.Count(); i++)
            {
                //if (string.IsNullOrEmpty(allMovies.ElementAt(i).Poster))
                //{
                    var tempMovie = importer.GetCompleteMovie(allMovies.ElementAt(i).Title);
                    allMovies.ElementAt(i).Poster = tempMovie.Poster;
                    allMovies.ElementAt(i).Plot = tempMovie.Plot;
                    allMovies.ElementAt(i).TomatoeImage = tempMovie.TomatoeImage;
                //}
                _context.SaveChanges();
            }

            //foreach (var movie in allMovies)
            //{
            //    if (string.IsNullOrEmpty(movie.Poster))
            //    {
            //        var tempMovie = importer.GetCompleteMovie(movie.Title);
            //        movie.Poster = tempMovie.Poster;
            //    }
            //_context.SaveChanges();
            //}
            //throw new NotImplementedException();
        }
    }
}

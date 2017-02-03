using FilmFind.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmFind.Services
{
    public interface IMovieData
    {
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> GetLatest(int count);
        IEnumerable<Movie> GetTopRated(int count);
        IEnumerable<Movie> GetMostHyped(int count);
        IEnumerable<Movie> Search(string searchString);
        Movie Get(int id);
        Movie GetComplete(int id);
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

        public Movie Add(Movie newMovie)
        {
            _context.Add(newMovie);
            _context.SaveChanges();
            return newMovie;
        }

        public void Remove(Movie movie)
        {
            _context.AllMovies.Remove(movie);
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

        public Movie Get(int id)
        {
            //var userMovies_test = _context.UserMovies.ToList();
            //var userMovies = _context.UserMovies.Where(um => um.MovieId == id).ToList();//future!
            var movie = _context.AllMovies.
                Where(m => m.Id == id).
                //Include(m => m.AddedByUsers.Select(s => s.UserMovies)).
                //Include("AddedByUsers").
                //Select(x=> new { Movie = x, Reviews = x.AddedByUsers.Select(y => y.UserMovies) }).
                FirstOrDefault();

            return movie;
        }

        public Movie GetComplete(int id)
        {
            //var temp = _context.UserMovies.ToList();
            var userMovies = _context.UserMovies.Where(um => um.MovieId == id).ToList();//future!
            //var comments = _context.UserMovies
            //    .Where(um => um.MovieId == id)
            //    .Include(um).ToList();//future!
            var movie = _context.AllMovies.
                Where(m => m.Id == id).
                //Include(m => m.AddedByUsers.Select(s => s.UserMovies)).
                Include("AddedByUsers").
                //Select(x => new { Movie = x, Reviews = x.AddedByUsers.Select(y => y.UserMovies) }).
                FirstOrDefault();

            return movie;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.AllMovies;
        }

        public IEnumerable<Movie> GetLatest(int count)
        {
            var sortedList = _context.AllMovies.OrderByDescending(m => m.Created).ToList();
            return GetRange(sortedList, count);
        }

        public IEnumerable<Movie> GetMostHyped(int count)
        {
            var sortedList = _context.AllMovies.OrderByDescending(m => m.Hyped).ToList();
            return GetRange(sortedList, count);
        }

        public IEnumerable<Movie> GetTopRated(int count)
        {
            var sortedList = _context.AllMovies.OrderByDescending(m => m.AverageUserRating).ToList();
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
                    Where(um => um.UserRating != 0 ).
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
            return _context.AllMovies.Where(s => s.Title.Contains(searchString)).ToList();
            //throw new NotImplementedException();
        }
    }
}

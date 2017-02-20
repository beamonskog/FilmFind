using FilmFind.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmFind.Services
{
    public interface IUserMovieDataService
    {
        IEnumerable<UserMovie> GetAllUserMovies(string userId);
        IEnumerable<UserMovie> GetLatest(int count);
        UserMovie Get(string userId, int movieId);
        Task<UserMovie> Add(string userId, UserMovie newMovie);
        //Task<UserMovie> RemoveUserMovie(string userId, UserMovie newMovie);
        Task<bool> Remove(string userId, UserMovie existingMovie);

        UserMovie AddHype(UserMovie userMovie);
        UserMovie RemoveHype(UserMovie userMovie);

        UserMovie AddFavorite(UserMovie userMovie);
        UserMovie RemoveFavorite(UserMovie userMovie);

        void Commit();
    }

    public class UserMovieDataService : IUserMovieDataService
    {
        private FilmFindDbContext _dbContext;
        private UserManager<User> _userManager;

        public UserMovieDataService(FilmFindDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public UserMovie AddHype(UserMovie userMovie)
        {
            if (userMovie.Hyped)
            {
                throw new Exception("Movie is already hyped!");
            }

            userMovie.Hyped = true;
            return userMovie;
        }

        public UserMovie RemoveHype(UserMovie userMovie)
        {
            if (!userMovie.Hyped)
            {
                throw new Exception("Movie isn't hyped!");
            }

            userMovie.Hyped = false;
            return userMovie;
        }

        public UserMovie RemoveFavorite(UserMovie userMovie)
        {
            if (!userMovie.Favored)
            {
                throw new Exception("Movie isn't favored!");
            }

            userMovie.Favored = false;
            return userMovie;
        }

        public UserMovie AddFavorite(UserMovie userMovie)
        {
            if (userMovie.Favored)
            {
                throw new Exception("Movie is already favored!");
            }

            userMovie.Favored = true;
            return userMovie;
        }

        public async Task<UserMovie> Add(string userId, UserMovie newMovie)
        {
            var currentUser = GetCurrentUser(userId);
            if (currentUser.UserMovies.Any(m => m.MovieId == newMovie.MovieId))
            {
                throw new Exception($"Movie \"{newMovie.MovieId}\" is already in your list!");
            }

            currentUser.UserMovies.Add(newMovie);
            var result = await _userManager.UpdateAsync(currentUser);

            return newMovie;
        }

        public async Task<bool> Remove(string userId, UserMovie existingMovie)
        {
            var currentUser = GetCurrentUser(userId);
            if (!currentUser.UserMovies.Any(m => m.MovieId == existingMovie.MovieId))
            {
                throw new Exception($"Movie \"{existingMovie.MovieId}\" is not in your list!");
            }

            currentUser.UserMovies.Remove(existingMovie);
            var result = await _userManager.UpdateAsync(currentUser);
            return true;
        }

        public UserMovie Get(string userId, int movieId)
        {
            var currentUser = GetCurrentUser(userId);
            if (currentUser.UserMovies!= null)
            {
                return currentUser.UserMovies.Where(m => m.MovieId == movieId).FirstOrDefault();
            }
            return null;
        }

        private User GetCurrentUser(string userId)
        {
            var currentUser = _dbContext.Users.Where(m => m.Id == userId);
            var currentUserPlus = currentUser.Include(c => c.UserMovies);

            return currentUserPlus.FirstOrDefault();
        }

        public IEnumerable<UserMovie> GetAllUserMovies(string userId)
        {
            var currentUser = GetCurrentUser(userId);
            return currentUser.UserMovies;
        }

        public IEnumerable<UserMovie> GetLatest(int count)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}

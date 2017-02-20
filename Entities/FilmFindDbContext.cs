using FilmFind.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmFind.Entities
{
    public class FilmFindDbContext : IdentityDbContext<User>
    {
        public FilmFindDbContext(DbContextOptions options) ://Singleton i Startup, addServices
            base(options)
        {   

        }
        public DbSet<GenreData> Genres { get; set; }
        //public DbSet<Genre> Departments { get; set; }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        //public DbSet<UserMovieRating> UserMovieRatings { get; set; }
        //public DbSet<UserMovieComment> UserMovieComments { get; set; }
    }
}

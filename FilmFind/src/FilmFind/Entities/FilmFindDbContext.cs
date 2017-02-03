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
        public DbSet<Movie> AllMovies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        //public DbSet<UserMovieRating> UserMovieRatings { get; set; }
        //public DbSet<UserMovieComment> UserMovieComments { get; set; }
    }
}

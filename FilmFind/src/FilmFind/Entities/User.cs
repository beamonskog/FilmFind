using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmFind.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserMovie> UserMovies { get; set; }
        public Banana MyFruit { get; set; }
        public User()
        {

        }
    }
}

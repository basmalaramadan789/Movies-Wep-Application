using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MoviesApp.Model
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options):base(Options)
        {
             
        }
       public DbSet<Genre> Genres { get; set; }
       public DbSet<Movie> Movies { get; set; }



    }
}

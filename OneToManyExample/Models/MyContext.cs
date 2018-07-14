using Microsoft.EntityFrameworkCore;
namespace OneToManyExample.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
       // public DbSet<Review> reviews { get; set; }
        public DbSet<Artist> artists { get; set; }
        public DbSet<Album> albums { get; set; }
    }
}
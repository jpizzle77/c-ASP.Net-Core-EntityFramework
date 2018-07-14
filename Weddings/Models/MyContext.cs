using Microsoft.EntityFrameworkCore;
namespace Weddings.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        //public DbSet<Review> reviews { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Wedding> weddings { get; set; }
        public DbSet<Rsvp> rsvps { get; set; }
    }
}
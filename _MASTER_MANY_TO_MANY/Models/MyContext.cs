using Microsoft.EntityFrameworkCore;
namespace Weddings.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        //public DbSet<Review> reviews { get; set; }
        public DbSet<User> users { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
namespace REST2.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<Review> reviews { get; set; }
    }
}
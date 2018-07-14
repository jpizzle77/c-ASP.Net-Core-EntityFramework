using Microsoft.EntityFrameworkCore;
namespace OneToManyExample.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<Review> reviews { get; set; }
    }
}
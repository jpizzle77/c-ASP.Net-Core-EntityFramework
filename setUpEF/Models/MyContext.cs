using Microsoft.EntityFrameworkCore;

namespace setUpEF.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<User> users { get; set; }
    }
}
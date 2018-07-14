using Microsoft.EntityFrameworkCore;
namespace dashboard.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<User> users { get; set; }
        public DbSet<Message> messages { get; set; }
        //public DbSet<Rsvp> rsvps { get; set; }
    }
}
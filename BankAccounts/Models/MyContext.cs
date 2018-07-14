using Microsoft.EntityFrameworkCore;
namespace BankAccounts.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        //public DbSet<Review> reviews { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Transaction1> transactions { get; set; }
    }
}
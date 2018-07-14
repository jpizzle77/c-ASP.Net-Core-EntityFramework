using Microsoft.EntityFrameworkCore;
using RESTauranter.Models;

namespace RESTauranter.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<Review> restaurant_reviews { get; set; }
    }
}
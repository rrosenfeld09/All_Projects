using Microsoft.EntityFrameworkCore;

namespace FitnessChallenge.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) {}

        public DbSet<User> users {get; set;}

        public DbSet<Log> logs {get; set;}

        public DbSet<Comment> comments {get; set;}

    }
}
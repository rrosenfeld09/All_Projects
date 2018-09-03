using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<User> users {get; set;}

        public DbSet<Wedding> weddings {get; set;}

        public DbSet<RSVP> rsvps {get; set;}

    }
}
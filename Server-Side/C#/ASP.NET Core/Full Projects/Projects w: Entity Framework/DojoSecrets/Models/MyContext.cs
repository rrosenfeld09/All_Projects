using Microsoft.EntityFrameworkCore;

namespace DojoSecrets.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<User> users {get; set;}

        public DbSet<Secret> secrets {get; set;}

        public DbSet<Like> likes {get; set;}
    }
}
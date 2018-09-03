using Microsoft.EntityFrameworkCore;
using LoginRegistration.Models;

namespace LoginRegistration.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}
    }
}
using Microsoft.EntityFrameworkCore;

namespace Email.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) {}

        public DbSet<User> users {get; set;}

        public DbSet<EmailMessage> emails {get; set;}

        public DbSet<Reply> replies {get; set;}

    }
}
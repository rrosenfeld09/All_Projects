using Microsoft.EntityFrameworkCore;
using budget.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    public DbSet<User> users {get; set;}

    public DbSet<PasswordResetCode> password_reset_codes {get; set;}
}
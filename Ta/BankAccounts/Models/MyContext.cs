using Microsoft.EntityFrameworkCore;
using BankAccounts.Models;

namespace BankAccounts.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> Users {get; set;}
        public DbSet<Transaction> Transactions {get; set;}

    }
}
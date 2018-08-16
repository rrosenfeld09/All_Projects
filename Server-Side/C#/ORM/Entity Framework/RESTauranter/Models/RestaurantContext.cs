using Microsoft.EntityFrameworkCore;
using RESTauranter.Models;

namespace RESTauranter.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<Review> Reviews {get; set;}
    }
}
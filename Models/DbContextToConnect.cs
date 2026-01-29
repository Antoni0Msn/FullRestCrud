using Microsoft.EntityFrameworkCore;
namespace WebApplication3.Models
{
    public class DbContextToConnect : DbContext
    {

        public DbContextToConnect(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ProductClass> productClasses { get; set; }

    }
}

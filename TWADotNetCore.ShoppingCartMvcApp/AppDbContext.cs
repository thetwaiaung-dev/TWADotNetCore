using Microsoft.EntityFrameworkCore;
using TWADotNetCore.ShoppingCartMvcApp.Models;

namespace TWADotNetCore.ShoppingCartMvcApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductDataModel> Products { get; set; }
    }
}

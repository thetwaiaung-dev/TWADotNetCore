using Microsoft.EntityFrameworkCore;
using TWADotNetCore.MinimalApi.Models;

namespace TWADotNetCore.MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}

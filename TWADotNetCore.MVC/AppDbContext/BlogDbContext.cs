using Microsoft.EntityFrameworkCore;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.AppDbContext
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; } 
    }


}

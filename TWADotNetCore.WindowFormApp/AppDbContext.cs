using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWADotNetCore.WindowFormApp.Models;

namespace TWADotNetCore.WindowFormApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<BlogModel> Blog { get; set; }
    }
}

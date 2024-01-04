using TWADotNetCore.ATMWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TWADotNetCore.ATMWebApp
{
    public class AtmDbContext : DbContext
    {
        public AtmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<AtmCardModel> AtmCard { get; set; }
    }
}

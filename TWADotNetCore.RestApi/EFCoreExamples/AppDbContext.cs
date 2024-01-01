using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TWADotNetCore.RestApi.Models;

namespace TWADotNetCore.RestApi.EFCoreExamples
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        //private readonly SqlConnectionStringBuilder _connection = new SqlConnectionStringBuilder()
        //{
        //    DataSource = ".",
        //    InitialCatalog = "AHMTZDotNetCore",
        //    UserID = "sa",
        //    Password = "thetwaiaung",
        //    TrustServerCertificate = true,
        //};

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(_connection.ConnectionString);
        //    }
        //}
        public DbSet<BlogModel> Blogs { get; set; }
    }
}

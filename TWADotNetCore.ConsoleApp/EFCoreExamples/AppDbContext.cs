using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TWADotNetCore.ConsoleApp.Models;

namespace TWADotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext : DbContext
    {
        private readonly SqlConnectionStringBuilder _connection = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AHMTZDotNetCore",
            UserID = "sa",
            Password = "thetwaiaung"
        };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connection.ConnectionString);
            }
        }

        public DbSet<BlogModel> BlogModels { get; set; }
    }
}

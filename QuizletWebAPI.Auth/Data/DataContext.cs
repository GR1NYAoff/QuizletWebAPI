using Microsoft.EntityFrameworkCore;
using QuizletWebAPI.Auth.Configuration;
using QuizletWebAPI.Auth.Models;

namespace QuizletWebAPI.Auth.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Access> Accesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data for Tests Table
            _ = modelBuilder.ApplyConfiguration(new TestsConfiguration());
        }
    }
}

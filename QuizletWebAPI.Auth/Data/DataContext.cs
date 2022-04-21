using Microsoft.EntityFrameworkCore;
using QuizletWebAPI.Auth.Models;

namespace QuizletWebAPI.Auth.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Access> Accesses { get; set; }
    }
}

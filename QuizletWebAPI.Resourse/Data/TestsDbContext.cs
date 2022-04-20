using Microsoft.EntityFrameworkCore;
using QuizletWebAPI.Resourse.Models;

namespace QuizletWebAPI.Resourse.Data
{
    public class TestsDbContext : DbContext
    {
        public TestsDbContext(DbContextOptions<TestsDbContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Access> Accesses { get; set; }
    }
}

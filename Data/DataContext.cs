using GRT.Entities;
using Microsoft.EntityFrameworkCore;

namespace GRT.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Result> Results { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}

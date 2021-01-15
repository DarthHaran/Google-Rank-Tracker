using GRT.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

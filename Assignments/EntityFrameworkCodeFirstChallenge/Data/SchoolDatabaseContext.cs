using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCodeFirstChallenge.Data {
  public class SchoolDatabaseContext : DbContext {
    public SchoolDatabaseContext(DbContextOptions<SchoolDatabaseContext> options)
        : base(options) {
      Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Student> Students { get; set; } = default!;
  }
}

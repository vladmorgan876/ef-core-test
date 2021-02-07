using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp2
{
  public class ApplicationContext :DbContext
  {
    public DbSet<Company> Companies { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    public ApplicationContext()
    {
      //Database.EnsureDeleted();
      //Database.EnsureCreated();
      //Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyConsoleApp2db;Trusted_Connection=True;");
    }
  }
}

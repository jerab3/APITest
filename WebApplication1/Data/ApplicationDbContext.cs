using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Organization> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Organization>().HasData(
            new Organization { Id = 1, Name = "Gatema", NumberOfEmployees = 100 },
            new Organization { Id = 2, Name = "Alza", NumberOfEmployees = 600 },
            new Organization { Id = 3, Name = "Google", NumberOfEmployees = 250000 }
        );
    }
}

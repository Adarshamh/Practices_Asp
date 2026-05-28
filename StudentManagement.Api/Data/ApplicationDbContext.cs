using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Entities;

namespace StudentManagement.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .Property(x => x.TotalFees)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Student>()
            .Property(x => x.FeesPaid)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Student>()
            .Property(x => x.FeesRemaining)
            .HasPrecision(18, 2);
    }
}
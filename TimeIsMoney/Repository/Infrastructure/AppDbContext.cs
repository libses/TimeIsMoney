using Microsoft.EntityFrameworkCore;
using TimeIsMoney.Repository.Models;

namespace TimeIsMoney.Repository.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<UserSalaryDbo> UserSalaries => Set<UserSalaryDbo>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserSalaryDbo>()
            .HasIndex(x => x.UserId)
            .IsUnique();
    }
}
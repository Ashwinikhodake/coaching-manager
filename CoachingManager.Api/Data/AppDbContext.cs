using CoachingManager.Api.Entities;
using Microsoft.EntityFrameworkCore;
namespace CoachingManager.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Institute> Institutes { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(u => new { u.InstituteId, u.Phone }).IsUnique();
        });
    }

}

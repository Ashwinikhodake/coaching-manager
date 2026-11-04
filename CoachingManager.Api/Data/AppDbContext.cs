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
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<StudentBatch> StudentBatches { get; set; } = null!;
    public DbSet<Batch> Batches { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(u => new { u.InstituteId, u.Phone }).IsUnique();
        });

        modelBuilder.Entity<Batch>(entity =>
        {
            entity.Property(b => b.MonthlyFee).HasPrecision(10, 2);
            entity.HasOne(b => b.Teacher).WithMany().HasForeignKey(b => b.TeacherId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<StudentBatch>(entity => entity.HasIndex(sb => new { sb.StudentId, sb.BatchId }).IsUnique());

    }

}

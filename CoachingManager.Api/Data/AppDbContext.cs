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
    public DbSet<FeePlan> FeePlans { get; set; } = null!;
    public DbSet<FeeDue> FeeDues { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Attendance> Attendances { get; set; } = null!;
    public DbSet<Notice> Notices { get; set; } = null!;

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

        modelBuilder.Entity<FeePlan>(entity =>
        {
            entity.Property(fp => fp.MonthlyAmount).HasPrecision(10, 2);
            entity.Property(fp => fp.Discount).HasPrecision(10, 2);
        });

        modelBuilder.Entity<FeeDue>(entity =>
        {
            entity.Property(fd => fd.AmountDue).HasPrecision(10, 2);
            entity.HasIndex(fd => new { fd.StudentId, fd.Month }).IsUnique();
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(p => p.Amount).HasPrecision(10, 2);
            entity.Property(p => p.Mode).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(p => new { p.InstituteId, p.ReceiptNo }).IsUnique();

            entity.HasOne(p => p.Institute).WithMany().HasForeignKey(p => p.InstituteId).OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Student).WithMany().HasForeignKey(p => p.StudentId).OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.FeeDue).WithMany(p => p.Payments).HasForeignKey(p => p.FeeDueId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.CreatedBy).WithMany().HasForeignKey(p => p.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.Property(a => a.Status).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(a => new { a.BatchId, a.StudentId, a.Date }).IsUnique();
            entity.HasOne(a => a.Batch).WithMany().HasForeignKey(a => a.BatchId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(a => a.Student).WithMany().HasForeignKey(a => a.StudentId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(a => a.MarkedBy).WithMany().HasForeignKey(a => a.MarkedByUserId).OnDelete(DeleteBehavior.Restrict);

        });


        modelBuilder.Entity<Notice>(entity =>
        {
            entity.HasOne(a => a.TargetBatch).WithMany().HasForeignKey(a => a.TargetBatchId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(a => a.CreatedBy).WithMany().HasForeignKey(a => a.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
        });
    }

}

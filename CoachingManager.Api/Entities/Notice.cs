using System.ComponentModel.DataAnnotations;

namespace CoachingManager.Api.Entities;

public class Notice
{
    public int Id { get; set; }
    public int InstituteId { get; set; }
    public Institute Institute { get; set; } = null!;
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;
    public int? TargetBatchId { get; set; }
    public Batch? TargetBatch { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
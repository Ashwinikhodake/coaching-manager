
using System.ComponentModel.DataAnnotations;
namespace CoachingManager.Api.Entities;

public class Batch
{
    public int Id { get; set; }
    public int InstituteId { get; set; }
    public Institute Institute { get; set; } = null!;
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Subject { get; set; } = string.Empty;
    [MaxLength(50)]
    public string ScheduleDays { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public decimal MonthlyFee { get; set; }
    public int? TeacherId { get; set; }
    public User? Teacher { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<StudentBatch> StudentBatches { get; set; } = new();

}
using System.ComponentModel.DataAnnotations;

namespace CoachingManager.Api.DTOs.Batches;

public class BatchRequest
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Subject { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string ScheduleDays { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    [Range(0, 100000)]
    public decimal MonthlyFee { get; set; }
    public int? TeacherId { get; set; }
}
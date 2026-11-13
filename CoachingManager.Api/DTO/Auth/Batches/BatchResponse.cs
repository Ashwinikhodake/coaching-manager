
namespace CoachingManager.Api.DTOs.Batches;

public class BatchResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;

    public string ScheduleDays { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public decimal MonthlyFee { get; set; }
    public int? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public int StudentCount { get; set; }
    public bool IsActive { get; set; }

}
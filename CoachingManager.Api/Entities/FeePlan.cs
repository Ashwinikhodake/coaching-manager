namespace CoachingManager.Api.Entities;

public class FeePlan
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public decimal MonthlyAmount { get; set; }
    public decimal Discount { get; set; } = 0;
    public int DueDay { get; set; }
    public DateOnly StartMonth { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
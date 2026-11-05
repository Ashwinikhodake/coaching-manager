namespace CoachingManager.Api.Entities;

public class FeeDue
{
    public int Id { get; set; }
    public int InstituteId { get; set; }
    public Institute Institute { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public DateOnly Month { get; set; }
    public decimal AmountDue { get; set; }
    public DateOnly DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Payment> Payments { get; set; } = new();

}
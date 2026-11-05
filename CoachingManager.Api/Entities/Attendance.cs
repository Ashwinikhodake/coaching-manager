namespace CoachingManager.Api.Entities;

public class Attendance
{
    public int Id { get; set; }
    public int BatchId { get; set; }
    public Batch Batch { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public DateOnly Date { get; set; }
    public AttendanceStatus Status { get; set; }
    public int MarkedByUserId { get; set; }
    public User MarkedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
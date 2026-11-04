namespace CoachingManager.Api.Entities;

public class StudentBatch
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public int BatchId { get; set; }
    public Batch Batch { get; set; } = null!;
    public DateOnly JoinDate { get; set; }
    public DateOnly? LeftOn { get; set; }


}
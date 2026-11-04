using System.ComponentModel.DataAnnotations;

namespace CoachingManager.Api.Entities;

public class Student
{
    public int Id { get; set; }
    public int InstituteId { get; set; }
    public Institute Institute { get; set; } = null!;
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Grade { get; set; } = string.Empty;
    [MaxLength(10)]
    public string? Phone { get; set; }

    [MaxLength(150)]
    public string ParentName { get; set; } = string.Empty;

    [MaxLength(10)]
    public string ParentPhone { get; set; } = string.Empty;
    [MaxLength(300)]
    public string? Address { get; set; }
    public DateOnly JoinDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<StudentBatch> StudentBatches { get; set; } = new();

}
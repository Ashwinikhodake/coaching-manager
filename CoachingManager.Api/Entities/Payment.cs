using System.ComponentModel.DataAnnotations;
namespace CoachingManager.Api.Entities;

public class Payment
{
    public int Id { get; set; }
    public int InstituteId { get; set; }
    public Institute Institute { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public int FeeDueId { get; set; }
    public FeeDue FeeDue { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateOnly PaidOn { get; set; }
    public PaymentMode Mode { get; set; }
    [MaxLength(30)]
    public string ReceiptNo { get; set; } = string.Empty;
    [MaxLength(300)]
    public string? Note { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
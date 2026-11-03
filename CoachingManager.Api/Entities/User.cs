using System;
using System.ComponentModel.DataAnnotations;

namespace CoachingManager.Api.Entities;

public class User
{
    public int Id { get; set; }
    public int InstituteId { get; set; }
    public Institute Institute { get; set; } = null!;
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(10)]
    public string Phone { get; set; } = string.Empty;
    [MaxLength(200)]
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
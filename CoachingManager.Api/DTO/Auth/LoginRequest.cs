using System.ComponentModel.DataAnnotations;

namespace CoachingManager.Api.DTOs.Auth;

public class LoginRequest
{
    [Required]
    [MaxLength(15)]
    public string Phone { get; set; } = string.Empty;
    [Required]
    [MaxLength(72)]
    public string Password { get; set; } = string.Empty;

}
using System.ComponentModel.DataAnnotations;

namespace CoachingManager.Api.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    [MaxLength(150)]
    public string InstituteName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string OwnerName { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be exactly 10 digits.")]
    public string Phone { get; set; } = string.Empty;
    [Required]
    [MinLength(8)]
    [MaxLength(72)]
    public string Password { get; set; } = string.Empty;


}
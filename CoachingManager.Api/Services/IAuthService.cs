using CoachingManager.Api.DTOs.Auth;
namespace CoachingManager.Api.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterSync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
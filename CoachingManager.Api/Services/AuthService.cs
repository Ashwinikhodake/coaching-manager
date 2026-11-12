using CoachingManager.Api.Data;
using CoachingManager.Api.DTOs.Auth;
using CoachingManager.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CoachingManager.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<AuthResponse> RegisterSync(RegisterRequest request)
    {
        var phoneTaken = await _db.Users.AnyAsync(u => u.Phone == request.Phone);
        if (phoneTaken)
            throw new InvalidOperationException("This phone number is already registered.");

        var institute = new Institute
        {
            Name = request.InstituteName.Trim(),
            OwnerName = request.OwnerName.Trim(),
            Phone = request.Phone,

        };

        var owner = new User
        {
            Institute = institute,
            Name = request.OwnerName.Trim(),
            Phone = request.Phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = Role.Owner
        };

        _db.Users.Add(owner);
        await _db.SaveChangesAsync();

        return BuildResponse(owner);
    }
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Phone == request.Phone);
        if (user is null || !user.IsActive || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid phone ");
        }
        return BuildResponse(user);
    }

    private AuthResponse BuildResponse(User user)
    {
        var key = _config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.");
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var minutes = int.Parse(_config["Jwt: ExpriryMinutes"] ?? "120");

        var expiresAt = DateTime.UtcNow.AddMinutes(minutes);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier ,user.Id.ToString()),
            new(ClaimTypes.Name ,user.Name),
            new(ClaimTypes.Role ,user.Role.ToString()),
            new("institute_id" ,user.InstituteId.ToString()),
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expiresAt,
            UserId = user.Id,
            Name = user.Name,
            Role = user.Role.ToString(),
            InstituteId = user.InstituteId,
        };


    }


}
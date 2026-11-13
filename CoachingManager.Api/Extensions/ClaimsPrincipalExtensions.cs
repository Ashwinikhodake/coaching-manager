using System.Security.Claims;

namespace CoachingManager.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (claim is null || !int.TryParse(claim.Value, out var id))
            throw new UnauthorizedAccessException("User id claim is missing or invalid.");

        return id;
    }

    public static int GetInstituteId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("institute_id");

        if (claim is null || !int.TryParse(claim.Value, out var id))
            throw new UnauthorizedAccessException("Institute id claim is missing or invalid.");

        return id;
    }
}
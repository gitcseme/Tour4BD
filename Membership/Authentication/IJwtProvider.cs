using Domain.Entities;
using System.Security.Claims;

namespace Membership.Authentication;

public interface IJwtProvider
{
    Task<(string AccessToken, string RefreshToken)> Generate(ClaimsPrincipal principal, ExtendedIdentityUser loggedInUser);
}

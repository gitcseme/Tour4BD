using Domain.Entities;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Membership.Authentication;

public interface IJwtProvider
{
    (string AccessToken, string RefreshToken) Generate(ClaimsPrincipal principal);
}

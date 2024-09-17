using Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IJwtProvider
{
    Task<(string AccessToken, string RefreshToken)> Generate(ClaimsPrincipal principal, ExtendedIdentityTenantUser loggedInUser);
    string GetConnectionStringFromToken();
}

using Application;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Membership.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtConfiguration _jwtConfig;
    private readonly ITenantDbContext _tenantDbContext;

    public JwtProvider(IOptions<JwtConfiguration> config, ITenantDbContext tenantDbContext)
    {
        _jwtConfig = config.Value;
        _tenantDbContext = tenantDbContext;
    }

    public async Task<(string AccessToken, string RefreshToken)> Generate(ClaimsPrincipal principal, ExtendedIdentityUser loggedInUser)
    {
        var claims = await PrepareClaimsAsync(principal, loggedInUser);

        var securiryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        var securityToken = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            //audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(securiryKey, SecurityAlgorithms.HmacSha256));

        string jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        string refreshToken = GenerateRefreshToken();
        return (jwtToken, refreshToken);
    }

    private async Task<List<Claim>> PrepareClaimsAsync(ClaimsPrincipal principal, ExtendedIdentityUser loggedInUser)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value),
            new Claim(JwtRegisteredClaimNames.Email, principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value)
        };

        var userPermissions = loggedInUser.Permissions
            .Select(p => new Claim(AppConstants.CustomClaim.Permissions, p.Name))
            .ToList();

        claims.AddRange(userPermissions);

        return claims;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}
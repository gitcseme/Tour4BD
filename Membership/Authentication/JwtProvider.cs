using Application;
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

    public JwtProvider(IOptions<JwtConfiguration> config)
    {
        _jwtConfig = config.Value;
    }

    public (string AccessToken, string RefreshToken) Generate(ClaimsPrincipal principal)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value),
            new Claim(JwtRegisteredClaimNames.Email, principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value)
        };

        foreach (var claim in principal.Claims.Where(c => c.Type == AppConstants.CustomClaim.Permissions))
        {
            claims.Add(claim);
        }

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

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}
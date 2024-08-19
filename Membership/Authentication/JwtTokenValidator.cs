using Application;
using Domain.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Membership.Authentication;

public class JwtTokenValidator
{
    private readonly JwtConfiguration _jwtConfig;

    public JwtTokenValidator(IOptions<JwtConfiguration> config)
    {
        _jwtConfig = config.Value;
    }

    public string ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameter = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtConfig?.Issuer,
            //ValidAudience = jwtConfig.Audience
            ClockSkew = TimeSpan.FromSeconds(0),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig?.SecretKey!))
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameter, out var validatedToken);
            var claims = principal.Claims;

            var tenantConnectionStringEncrypted = claims.FirstOrDefault(c => c.Type == AppConstants.CustomClaim.TenantConnectionString)?.Value;
            return EncryptionHelper.Decrypt(tenantConnectionStringEncrypted!);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

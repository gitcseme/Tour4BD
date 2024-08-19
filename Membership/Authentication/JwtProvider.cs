using Application;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
    private readonly IUnitOfWork _uow;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtTokenValidator _tokenValidator;

    public JwtProvider(IOptions<JwtConfiguration> config,
        [FromKeyedServices(AppConstants.TenantDbContextDIKey)] IUnitOfWork uow,
        IHttpContextAccessor httpContextAccessor,
        JwtTokenValidator tokenValidator)
    {
        _jwtConfig = config.Value;
        _uow = uow;
        _httpContextAccessor = httpContextAccessor;
        _tokenValidator = tokenValidator;
    }

    public async Task<(string AccessToken, string RefreshToken)> Generate(ClaimsPrincipal principal, ExtendedIdentityTenantUser loggedInUser)
    {
        var claims = await PrepareClaimsAsync(principal, loggedInUser);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        var securityToken = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            //audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

        string jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        string refreshToken = GenerateRefreshToken();
        return (jwtToken, refreshToken);
    }

    private async Task<List<Claim>> PrepareClaimsAsync(ClaimsPrincipal principal, ExtendedIdentityTenantUser loggedInUser)
    {
        var tenant = await _uow.Repository<Tenant, int>().GetAsync(loggedInUser.TenantId);
        if (tenant is null)
        {
            throw new Exception($"Tenant with id = {loggedInUser.TenantId} not found");
        }

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value),
            new Claim(JwtRegisteredClaimNames.Email, principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value),
            new Claim(AppConstants.CustomClaim.TenantConnectionString, EncryptionHelper.Encrypt(tenant.ConnectionString))
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

    public string GetConnectionStringFromToken()
    {
        var authData = _httpContextAccessor.HttpContext.Items[AppConstants.TokenItem] ?? throw new Exception("User is not authenticated");
        var accessToken = authData as string;
        var tenantConnectionString = _tokenValidator.ValidateToken(accessToken!);
        
        return tenantConnectionString;
    }
}
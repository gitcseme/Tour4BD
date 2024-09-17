using Application.DTOs;
using Domain.Entities;

namespace Membership;

public interface IAccountService
{
    Task<ExtendedIdentityTenantUser> CreateUser(string email, string password, int tenantId);
    Task<ExtendedIdentityTenantUser> CreateUserAndAssignAdminRoleAsync(string email, string password, int tenantId);
    Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
}

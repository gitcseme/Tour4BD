using Application.DTOs;
using Domain.Entities;

namespace Membership;

public interface IAccountService
{
    Task<ExtendedIdentityUser> CreateUser(string email, string password, int tenantId);
    Task<ExtendedIdentityUser> CreateUserAndAssignAdminRoleAsync(string email, string password, int tenantId);
    Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
}

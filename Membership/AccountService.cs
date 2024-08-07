using Application;
using Application.DTOs;

using Domain.Entities;

using Membership.Authentication;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Membership;

public class AccountService : IAccountService
{
    private readonly UserManager<ExtendedIdentityUser> _userManager;
    private readonly SignInManager<ExtendedIdentityUser> _signInManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(UserManager<ExtendedIdentityUser> userManager, SignInManager<ExtendedIdentityUser> signInManager, IJwtProvider jwtProvider, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtProvider = jwtProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ExtendedIdentityUser> CreateUser(string email, string password, int tenantId)
    {
        var user = new ExtendedIdentityUser(email, tenantId);
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return await Task.FromResult(user);
        }

        throw new Exception("Error creating user");
    }

    public async Task<ExtendedIdentityUser> CreateUserAndAssignAdminRoleAsync(string email, string password, int tenantId)
    {
        var user = await CreateUser(email, password, tenantId);
        await _userManager.AddToRoleAsync(user, AppConstants.Roles.Admin);
        return user;
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager
                .Users
                .AsNoTracking()
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user is null)
        {
            return ApiResponse<LoginResponse>.Failure("No user found with this email");
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (signInResult.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }
        else
        {
            return ApiResponse<LoginResponse>.Failure("Failed to login, invalid credentials");
        }

        var tokenResult = await _jwtProvider.Generate(_httpContextAccessor.HttpContext.User, user);

        return ApiResponse<LoginResponse>.Success(new LoginResponse
        {
            User = new UserResponse(user),
            AccessToken = tokenResult.AccessToken,
            RefreshToken = tokenResult.RefreshToken
        });
    }
}

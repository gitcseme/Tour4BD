using Application.Features.Auth.Models;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKarnel.Contracts;
using SharedKarnel.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands;

public record LoginCommand(string Email, string Password, bool RememberMe) : ICommand<LoginResponse>;

public class LoginCommandHandler(
    SignInManager<SystemUser> signInManager,
    IJwtProvider jwtProvider,
    IHttpContextAccessor _httpContextAccessor,
    IUnitOfWork uow)
    : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand cmd, CancellationToken ctn)
    {
        var systemUser = await uow.GetTable<SystemUser>()
            .AsNoTracking()
            .Include(u => u.UserPermissions)
                .ThenInclude(up => up.Permission)
            .FirstOrDefaultAsync(u => u.Email == cmd.Email, ctn);

        if (systemUser == null)
        {
            return Result<LoginResponse>.Failure(message: "User not found");
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(systemUser, cmd.Password, lockoutOnFailure: false);

        if (!signInResult.Succeeded)
        {
            return Result<LoginResponse>.Failure("Failed to login, invalid credentials");
        }

        await signInManager.SignInAsync(systemUser, isPersistent: cmd.RememberMe);

        var tokenResult = await jwtProvider.Generate(_httpContextAccessor.HttpContext.User, systemUser);
        var loginResponse = new LoginResponse(new UserResponse(systemUser), tokenResult.AccessToken, tokenResult.RefreshToken);

        return Result<LoginResponse>.Success(loginResponse);
    }
}

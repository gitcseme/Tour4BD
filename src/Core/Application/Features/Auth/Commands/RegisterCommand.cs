using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKarnel.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands;

public record RegisterCommand(string Email, string Password) : IRequest<Result<bool>>;

public class RegisterCommandHandler(UserManager<SystemUser> userManager)
    : IRequestHandler<RegisterCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken ctn)
    {
        var systemUser = new SystemUser(request.Email);
        var result = await userManager.CreateAsync(systemUser, request.Password);

        return result.Succeeded 
            ? Result<bool>.Success(true) 
            : Result<bool>.Failure(result.Errors, "registration failed");
    }
}

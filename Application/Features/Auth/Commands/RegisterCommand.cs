using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using SharedKarnel.Contracts;
using SharedKarnel.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands;

public record RegisterCommand(string Email, string Password) : ICommand<bool>;

public class RegisterCommandHandler(UserManager<SystemUser> userManager)
    : ICommandHandler<RegisterCommand, bool>
{
    public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken ctn)
    {
        var systemUser = new SystemUser(request.Email);
        var result = await userManager.CreateAsync(systemUser, request.Password);

        return result.Succeeded 
            ? Result<bool>.Success(result.Succeeded) 
            : Result<bool>.Failure(result.Errors, "registration failed");
    }
}

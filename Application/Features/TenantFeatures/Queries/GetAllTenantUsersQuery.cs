using Application.Features.TenantFeatures.Responses;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TenantFeatures.Queries;

public record GetAllTenantUsersQuery(int TenantId) : IRequest<IEnumerable<TenantUserResponse>>;

public class GetAllTenantUsersQueryHandler : IRequestHandler<GetAllTenantUsersQuery, IEnumerable<TenantUserResponse>>
{
    private readonly UserManager<ExtendedIdentityTenantUser> _userManager;

    public GetAllTenantUsersQueryHandler(UserManager<ExtendedIdentityTenantUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<TenantUserResponse>> Handle(GetAllTenantUsersQuery request, CancellationToken cancellationToken)
    {
        var tenantUsers = await _userManager.Users
            .Where(u => u.TenantId == request.TenantId)
            .Select(u => new TenantUserResponse()
            {
                Id = u.Id,
                TenantId = u.TenantId,
                Email = u.Email,
                Username = u.UserName
            })
            .ToListAsync(cancellationToken);

        return tenantUsers;
    }
}
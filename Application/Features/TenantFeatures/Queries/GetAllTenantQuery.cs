using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Application.Features.TenantFeatures.Responses;
using Application.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.Queries;

public record GetAllTenantQuery : IRequest<IEnumerable<TenantResponseDto>>;

public class GetAllTenantHandler : IRequestHandler<GetAllTenantQuery, IEnumerable<TenantResponseDto>>
{
    private readonly ITenantDbContext _tenantDbContext;

    public GetAllTenantHandler(ITenantDbContext tenantDbContext)
    {
        _tenantDbContext = tenantDbContext;
    }

    public async Task<IEnumerable<TenantResponseDto>> Handle(GetAllTenantQuery query, CancellationToken cancellationToken)
    {
        var tenants = await _tenantDbContext.Tenants
            .Select(t => new TenantResponseDto
            {
                Id = t.Id,
                OrganizationName = t.OrganizationName,
                ConnectionString = t.ConnectionString
            })
            .ToListAsync(cancellationToken);

        return tenants;
    }
}
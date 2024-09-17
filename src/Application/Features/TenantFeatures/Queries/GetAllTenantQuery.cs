using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Application.Features.TenantFeatures.Responses;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Features.TenantFeatures.Queries;

public record GetAllTenantQuery : IRequest<IEnumerable<TenantResponseDto>>;

public class GetAllTenantQueryHandler : IRequestHandler<GetAllTenantQuery, IEnumerable<TenantResponseDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllTenantQueryHandler([FromKeyedServices(AppConstants.TenantDbContextDIKey)] IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<TenantResponseDto>> Handle(GetAllTenantQuery query, CancellationToken ctn)
    {
        var tenants = await _uow.Repository<Tenant, int>()
            .Query()
            .Select(t => new TenantResponseDto
            {
                Id = t.Id,
                OrganizationName = t.OrganizationName,
                ConnectionString = t.ConnectionString
            })
            .ToListAsync(ctn);

        return tenants;
    }
}
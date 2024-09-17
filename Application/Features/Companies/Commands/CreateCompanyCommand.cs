using Application.DTOs;
using Application.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;
using Microsoft.Extensions.DependencyInjection;

using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Companies.Commands;

public class CreateCompanyCommand : IRequest<ApiResponse<int>>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LisenceLink { get; set; } = string.Empty;
    public int OwnerId { get; set; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ApiResponse<int>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler([FromKeyedServices(AppConstants.ApplicationDbContextDIKey)] IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<int>> Handle(CreateCompanyCommand command, CancellationToken ct = default)
    {
        var companyEntity = _mapper.Map<Company>(command);
        await _uow.Repository<Company, int>().AddAsync(companyEntity, ct);
        await _uow.SaveAsync();
        return ApiResponse<int>.Success(companyEntity.Id, AppConstants.DefaultMessages.CreateSuccess);
    }
}
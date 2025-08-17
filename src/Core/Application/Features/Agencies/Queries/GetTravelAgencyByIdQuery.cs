using Application.Abstructions;
using Application.Common;
using Application.Features.Agencies.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharedKernel.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Agencies.Queries;

public class GetTravelAgencyByIdQuery : IRequest<Result<TravelAgencyDetailModel>>
{
    public int Id { get; set; }
}

public sealed class GetTravelAgencyByIdQueryHandler
    : BaseRequestHandler<GetTravelAgencyByIdQuery, Result<TravelAgencyDetailModel>, TravelAgency, int>
{
    private readonly IDistributedCache _cache;

    public GetTravelAgencyByIdQueryHandler(IUnitOfWork uow, IMapper mapper, IDistributedCache cache) : base(uow, mapper)
    {
        _cache = cache;
    }

    public override async Task<Result<TravelAgencyDetailModel>> HandleRequest(
        GetTravelAgencyByIdQuery request,
        CancellationToken ctn)
    {
        var cacheKey = $"TravelAgency-{request.Id}";
        var travelAgency = await _cache.GetOrCreateAsync(cacheKey, async options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            options.SlidingExpiration = TimeSpan.FromMinutes(3);

            return await Repository.GetAsync(request.Id, ctn);
        });

        return travelAgency is null
            ? Result<TravelAgencyDetailModel>.Failure(message: DataNotFound(request.Id))
            : _mapper.Map<TravelAgencyDetailModel>(travelAgency);
    }
}

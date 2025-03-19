using Application.Abstructions;
using Application.Common;
using Application.Features.Agencies.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedKarnel.Contracts;
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
    public GetTravelAgencyByIdQueryHandler(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
    {
    }

    public override async Task<Result<TravelAgencyDetailModel>> HandleRequest(
        GetTravelAgencyByIdQuery request,
        CancellationToken ctn)
    {
        var travelAgency = await Repository.GetAsync(request.Id, ctn);
        if (travelAgency is null)
        {
            return Result<TravelAgencyDetailModel>.Failure(message: DataNotFound(request.Id));
        }

        return _mapper.Map<TravelAgencyDetailModel>(travelAgency);
    }
}
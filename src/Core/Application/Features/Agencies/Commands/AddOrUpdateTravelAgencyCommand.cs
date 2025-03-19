using Application.Abstructions;
using Application.Common;
using Application.Features.Agencies.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedKarnel;
using SharedKarnel.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Agencies.Commands;

public class AddOrUpdateTravelAgencyCommand : TravelAgencyBaseModel, IRequest<Result<int>>
{
}

public sealed class AddTravelAgencyCommandHandler
    : BaseRequestHandler<AddOrUpdateTravelAgencyCommand, Result<int>, TravelAgency, int>
{
    public AddTravelAgencyCommandHandler(IUnitOfWork uow, IMapper mapper)
        : base(uow, mapper)
    {
    }

    public override async Task<Result<int>> HandleRequest(
        AddOrUpdateTravelAgencyCommand request,
        CancellationToken ctn)
    {
        var travelAgency = request.Id != 0 ? await Repository.GetAsync(request.Id, ctn) : new();
        if (travelAgency is null)
        {
            return Result<int>.Failure(DataNotFound(request.Id));
        }

        _mapper.Map(request, travelAgency);

        if (request.Id == 0)
        {
            await Repository.AddAsync(travelAgency, ctn);
        }

        await _uow.SaveAsync(ctn);

        return Result<int>.Success(travelAgency.Id,
            request.Id == 0? AppConstants.CreateSuccess : AppConstants.UpdateSuccess);
    }
}
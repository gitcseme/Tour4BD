using Application.Abstructions;
using Application.Common;
using Application.Features.Agencies.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedKernel;
using SharedKernel.Contracts;
using SharedKernel.Grids;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Agencies.Queries;

public class GetAllTravelAgencyQuery 
    : GridDataFetchRequest, IRequest<Result<ListResponse<TravelAgencyListModel>>>
{
}

public sealed class GetAllTravelAgencyQueryHandler
    : BaseGridRequestHandler<GetAllTravelAgencyQuery, Result<ListResponse<TravelAgencyListModel>>, TravelAgency, int>
{
    public GetAllTravelAgencyQueryHandler(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
    {
    }

    public override async Task<Result<ListResponse<TravelAgencyListModel>>> HandleGridDataFetchRequest(
        GetAllTravelAgencyQuery request, 
        CancellationToken ctn)
    {
        var result = await GridDataFetcher.GetListAsync<TravelAgency, TravelAgencyListModel>(Query, request, _mapper, ctn);

        return result;
    }

    protected override Sort? GetDefaultSort()
    {
        return new Sort { FieldName = nameof(TravelAgency.Name), Direction = SortOrder.Asc.ToString() };
    }
}
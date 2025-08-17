using System.Threading;
using System.Threading.Tasks;
using Application.Abstructions;
using Application.Common;
using Application.Features.TourPackages.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedKernel.Contracts;
using SharedKernel.Grids;

namespace Application.Features.TourPackages.Queries;

public class GetAllPackagesQuery : GridDataFetchRequest, IRequest<Result<ListResponse<PackageListModel>>>
{
}

public sealed class GetAllPackagesQueryHandler
    : BaseGridRequestHandler<GetAllPackagesQuery, Result<ListResponse<PackageListModel>>, Package, int>
{
    public GetAllPackagesQueryHandler(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
    {
    }

    public override async Task<Result<ListResponse<PackageListModel>>> HandleGridDataFetchRequest(
        GetAllPackagesQuery request, CancellationToken ctn)
    {
        var result = await GridDataFetcher.GetListAsync<Package, PackageListModel>(Query, request, _mapper, ctn);

        return result;
    }
}
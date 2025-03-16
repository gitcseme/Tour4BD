using Application.Abstructions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedKarnel.Grids;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common;

public abstract class BaseGridRequestHandler<TRequest, TResponse, TEntity, TKey>
    : BaseRequestHandler<TRequest, TResponse, TEntity, TKey>
    where TRequest : GridDataFetchRequest, IRequest<TResponse>
    where TKey : IEquatable<TKey>
    where TEntity : EntityBase<TKey>
{
    protected BaseGridRequestHandler(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
    {
    }

    public override async Task<TResponse> HandleRequest(TRequest request, CancellationToken ctn)
    {
        if (request.Sort is null || string.IsNullOrEmpty(request.Sort.FieldName))
        {
            request.Sort = GetDefaultSort();
        }

        return await HandleGridDataFetchRequest(request, ctn);
    }

    public abstract Task<TResponse> HandleGridDataFetchRequest(TRequest request, CancellationToken ctn);

    protected virtual Sort? GetDefaultSort() => null;
}
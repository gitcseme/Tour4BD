using Application.Abstructions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common;

public abstract class BaseRequestHandler<TRequest, TResponse, TEntity, TKey>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TKey : IEquatable<TKey>
    where TEntity : EntityBase<TKey>

{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _uow;

    protected BaseRequestHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    private IRepository<TEntity, TKey> _repository;
    private IQueryable<TEntity> _query;

    protected IRepository<TEntity, TKey> Repository => _repository ??= _uow.GetRepository<TEntity, TKey>();
    protected IQueryable<TEntity> Query => _query ??= _uow.GetTable<TEntity>();

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return HandleRequest(request, cancellationToken);
    }

    public abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken ctn);

    protected string DataNotFound(TKey key) => $"{typeof(TEntity).Name} not found with id = '{key}'";
}

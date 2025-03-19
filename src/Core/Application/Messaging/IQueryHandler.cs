using MediatR;
using SharedKarnel.Contracts;

namespace Application.Messaging;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>> where TQuery : IQuery<TResult>;
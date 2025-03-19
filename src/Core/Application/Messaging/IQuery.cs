using MediatR;
using SharedKarnel.Contracts;

namespace Application.Messaging;

public interface IQuery<T> : IRequest<Result<T>>;

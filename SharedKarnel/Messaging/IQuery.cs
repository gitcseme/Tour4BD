using MediatR;
using SharedKarnel.Contracts;

namespace SharedKarnel.Messaging;

public interface IQuery<T> : IRequest<Result<T>>;

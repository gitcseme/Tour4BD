using MediatR;
using SharedKarnel.Contracts;

namespace SharedKarnel.Messaging;

public interface ICommand<T> : IRequest<Result<T>>;

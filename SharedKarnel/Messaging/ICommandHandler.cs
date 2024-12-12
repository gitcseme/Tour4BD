using MediatR;
using SharedKarnel.Contracts;

namespace SharedKarnel.Messaging;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
using MediatR;
using SharedKarnel.Contracts;

namespace Application.Messaging;

public interface ICommand<T> : IRequest<Result<T>>;

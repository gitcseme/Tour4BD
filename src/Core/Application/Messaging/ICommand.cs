using MediatR;
using SharedKernel.Contracts;

namespace Application.Messaging;

public interface ICommand<T> : IRequest<Result<T>>;

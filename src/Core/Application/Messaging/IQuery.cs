using MediatR;
using SharedKernel.Contracts;

namespace Application.Messaging;

public interface IQuery<T> : IRequest<Result<T>>;

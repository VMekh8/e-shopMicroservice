using MediatR;

namespace BuildingBlock.CQRS.Queries;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : ICommand<Unit>;
using MediatR;

namespace BuildingBlock.CQRS.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull;
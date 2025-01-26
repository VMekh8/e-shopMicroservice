using BuildingBlock.CQRS.Queries;
using MediatR;

namespace BuildingBlock.CQRS.QueryHandlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull;
using BuildingBlock.CQRS.Queries;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(ProductId Id) : ICommand<DeleteOrderCommandResult>;

public record DeleteOrderCommandResult(bool IsSuccess);

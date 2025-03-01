using BuildingBlock.CQRS.Queries;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderCommandResult>;

public record UpdateOrderCommandResult(bool IsSuccess);

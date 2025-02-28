using BuildingBlock.CQRS.Queries;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : 
    ICommand<CreateOrderCommandResult>;

public record CreateOrderCommandResult(Guid Id);
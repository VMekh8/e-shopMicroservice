﻿using BuildingBlock.CQRS.QueryHandlers;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CreateOrderCommandResult>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = CreateOrder(request.Order);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateOrderCommandResult(order.Id.Value);
    }

    private Order CreateOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode
        );

        var billingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode
        );

        var order = Order.Create(
            OrderId.Of(orderDto.Id),
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName), 
            shippingAddress,
            billingAddress,
            Payment.Of(orderDto.Payment.CardName, 
                orderDto.Payment.CardNumber, 
                orderDto.Payment.Expiration, 
                orderDto.Payment.Cvv, 
                orderDto.Payment.PaymentMethod)
        );

        foreach (var orderItemsDto in orderDto.OrderItems)
        {
            order.Add(
                ProductId.Of(orderItemsDto.ProductId),
                orderItemsDto.Price,
                orderItemsDto.Quantity
                );
        }

        return order;
    }
}
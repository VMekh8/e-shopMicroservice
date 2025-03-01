using BuildingBlock.CQRS.QueryHandlers;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, UpdateOrderCommandResult>
{
    private readonly IApplicationDbContext _context;

    public UpdateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateOrderCommandResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.OrderDto.Id);
        var order = await _context.Orders.FindAsync([orderId], cancellationToken);

        if (order == null)
        {
            throw new OrderNotFoundException(request.OrderDto.Id);
        }

        UpdateOrderWithNewValues(order, request.OrderDto);

        _context.Orders.Update(order);
        var savingResult =  await _context.SaveChangesAsync(cancellationToken);

        return new UpdateOrderCommandResult(savingResult > 0); 
    }

    private void UpdateOrderWithNewValues(Order order, OrderDto newOrder)
    {
        var updatedShippingAddress = Address.Of(
            newOrder.ShippingAddress.FirstName,
            newOrder.ShippingAddress.LastName,
            newOrder.ShippingAddress.EmailAddress,
            newOrder.ShippingAddress.AddressLine,
            newOrder.ShippingAddress.Country,
            newOrder.ShippingAddress.State,
            newOrder.ShippingAddress.ZipCode
        );

        var updatedBillingAddress = Address.Of(
            newOrder.BillingAddress.FirstName,
            newOrder.BillingAddress.LastName,
            newOrder.BillingAddress.EmailAddress,
            newOrder.BillingAddress.AddressLine,
            newOrder.BillingAddress.Country,
            newOrder.BillingAddress.State,
            newOrder.BillingAddress.ZipCode
        );

        var updatedPaymentMethod = Payment.Of(
            newOrder.Payment.CardName,
            newOrder.Payment.CardNumber,
            newOrder.Payment.Expiration,
            newOrder.Payment.Cvv,
            newOrder.Payment.PaymentMethod);

        order.Update(
            OrderName.Of(newOrder.OrderName), 
            updatedShippingAddress, 
            updatedBillingAddress, 
            updatedPaymentMethod, 
            newOrder.OrderStatus);
    }
}
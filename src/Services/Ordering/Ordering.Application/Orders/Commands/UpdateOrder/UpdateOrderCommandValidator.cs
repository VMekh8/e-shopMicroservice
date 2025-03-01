using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDto.Id).NotEmpty().WithMessage("Order id is required");
        RuleFor(x => x.OrderDto.OrderName).NotEmpty().WithMessage("Order name is required");
        RuleFor(x => x.OrderDto.CustomerId).NotNull().WithMessage("Customer id is required");
    }
}
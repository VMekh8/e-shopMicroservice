using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id`s is required");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("The name of product is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("The price of product must be greater than 0");
    }
}
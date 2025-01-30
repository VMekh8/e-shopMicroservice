using FluentValidation;

namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().WithMessage("The name of product is required");
        RuleFor(command => command.Category).NotEmpty().WithMessage("The category of product is required");
        RuleFor(command => command.ImageFile).NotEmpty().WithMessage("The image of product is required");
        RuleFor(command => command.Price).GreaterThan(0).WithMessage("The price of product must be greater than 0");
    }
}
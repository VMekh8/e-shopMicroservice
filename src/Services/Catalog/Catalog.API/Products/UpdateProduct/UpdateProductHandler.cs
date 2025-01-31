using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
    ) : ICommand<UpdateProductResponse>;

public record UpdateProductResponse(bool Updated);

public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IDocumentSession _documentSession;

    public UpdateProductHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(request.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Category = request.Category;
        product.ImageFile = request.ImageFile;
        product.Price = request.Price;
        
        _documentSession.Update(product);
        await _documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductResponse(Updated: true);
    }
}
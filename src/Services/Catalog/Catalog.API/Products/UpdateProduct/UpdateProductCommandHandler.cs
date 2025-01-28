using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Product Product) : ICommand<UpdateProductResponse>;

public record UpdateProductResponse(bool Updated);

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IDocumentSession _documentSession;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IDocumentSession documentSession)
    {
        _documentSession = documentSession;
        _logger = logger;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateProductCommand.Handler called for request with id: {id}", request.Product.Id);

        var product = await _documentSession.LoadAsync<Product>(request.Product.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = request.Product.Name;
        product.Description = request.Product.Description;
        product.Category = request.Product.Category;
        product.ImageFile = request.Product.ImageFile;
        product.Price = request.Product.Price;
        
        _documentSession.Update(product);
        await _documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductResponse(Updated: true);
    }
}
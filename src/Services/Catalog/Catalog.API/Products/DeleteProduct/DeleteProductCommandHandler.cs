using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResponse>;

public record DeleteProductResponse(bool Deleted);

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IDocumentSession _session;

    public DeleteProductCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _session.Delete<Product>(request.Id);
        await _session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResponse(Deleted: true);
    }
}
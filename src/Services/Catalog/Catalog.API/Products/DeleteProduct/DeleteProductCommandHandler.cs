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
    private readonly ILogger<DeleteProductCommandHandler> _logger;

    public DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteProductCommand.Handler called by command: {request}", request);

        _session.Delete<Product>(request.Id);
        await _session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResponse(true);
    }
}
using MediatR;

namespace Catalog.API.Products.CreateProduct;

public interface ICommand<out TResponse> : IRequest<TResponse>
{}

public interface ICommand : ICommand<Unit> {}
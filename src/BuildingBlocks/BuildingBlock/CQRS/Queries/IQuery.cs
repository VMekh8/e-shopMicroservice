using MediatR;

namespace Catalog.API.Products.CreateProduct;

public interface IQuery<out TResponse> : IRequest<TResponse> 
    where TResponse : notnull
{ }
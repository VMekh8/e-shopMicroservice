﻿using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
    );

public record CreateProductResponse(Guid Id);


public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = new CreateProductResponse(result.Id);

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .WithDescription("Creating Product")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product");
    }
}
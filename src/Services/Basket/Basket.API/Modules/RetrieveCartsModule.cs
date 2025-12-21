using Asp.Versioning;
using Basket.API.Dto.RemoveCart;
using Basket.API.Dto.RetrieveCart;
using Basket.API.Dto.SaveCart;
using Basket.API.Models.ShoppingCart;
using Basket.API.ShoppingBasket.Remove;
using Basket.API.ShoppingBasket.Retrieve;
using Basket.API.ShoppingBasket.Save;
using Carter;
using Carter.OpenApi;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RemoveCartResult = Basket.API.ShoppingBasket.Remove.RemoveCartResult;

namespace Basket.API.Modules;

public class RetrieveCartsModule : ControllerBase, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cart/{accountName}",
                    async (string accountName, ISender sender) =>
                    {
                        var query = new RetrieveCartQuery(accountName);
                        var result = await sender.Send(query);
                        var response = result.Adapt<RetrieveCartResponse>();
                        return Results.Ok(response);
                    })
                .WithName("RetrieveCart")
                .WithSummary("Retrieve the cart by user account")
                .WithDescription("Retrieve the cart items by user account")
                .Produces<RetrieveCartResponse>(StatusCodes.Status200OK)
                .IncludeInOpenApi();
        
        app.MapPost("/cart",
                    async ([FromBody] SaveCartRequest request, ISender sender) =>
                    {
                        var command = request.Adapt<SaveCartCommand>();
                        var result = await sender.Send(command);
                        var response = result.Adapt<SaveCartResponse>();
                        return Results.Created($"/cart/{response.AccountName}", response);
                    })
                .WithName("SaveCart")
                .WithSummary("Save the cart")
                .WithDescription("Save the cart items")
                .Produces<SaveCartResult>(StatusCodes.Status201Created)
                .IncludeInOpenApi();
        
        app.MapDelete("/cart/delete/{accountName}",
            async (string accountName, ISender sender) =>
            {
                var command = new RemoveCartCommand(accountName);
                var result = await sender.Send(command);
                var response = result.Adapt<RemoveCartResult>();
                return Results.Ok(result);
            })
            .WithName("RemoveCart")
            .WithSummary("Remove the cart")
            .WithDescription("Remove the cart by account name")
            .Produces<RemoveCartResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.ShoppingBasket.Checkout;

public class CheckoutCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart/checkout", async (
            [FromBody] CheckoutCartRequest request,
            ISender sender) =>
        {
            var correlationId = Guid.NewGuid().ToString();
            
            var command = request.Adapt<CheckoutCartCommand>();
            command = command with { CorrelationId = correlationId };
            
            var result = await sender.Send(command);
            
            var response = result.Adapt<CheckoutCartResponse>();
            return Results.Ok(response);
        });
    }
}
using Carter;
using Checkout.Application.Orders.Commands.CreateOrder;
using Checkout.Application.Orders.Commands.CreateOrder.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.API.Orders.CreateOrder;

public record CreateOrderRequest(CreateOrderDto OrderData);
public record CreateOrderResponse(CreateOrderResultDto Result);

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (
            [FromBody] CreateOrderRequest request,
            ISender sender
        ) =>
        {
            var command = new CreateOrderCommand(request.OrderData);
            var result = await sender.Send(command);
            var response = new CreateOrderResponse(result);

            return Results.Created($"/orders/{response.Result.OrderId}", response);
        });
    }
}
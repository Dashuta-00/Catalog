using Carter;
using Checkout.Application.Orders.Commands.DeleteOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.API.Orders.DeleteOrder;

public record DeleteOrderRequest(string OrderId);
public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders", async (
            [FromBody] DeleteOrderRequest request,
            ISender sender) =>
        {
            var command = new DeleteOrderCommand(request.OrderId);
            var result = await sender.Send(command);
            var response = new DeleteOrderResult(result.IsSuccess);
            return Results.Ok(response);
        });
    }
}
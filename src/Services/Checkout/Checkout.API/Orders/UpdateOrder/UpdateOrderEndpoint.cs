using Carter;
using Checkout.Application.Orders.Commands.UpdateOrder;
using Checkout.Application.Orders.Commands.UpdateOrder.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.API.Orders.UpdateOrder;

public record UpdateOrderRequest(UpdateOrderRequestDto RequestBody);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (
            [FromBody] UpdateOrderRequest request,
            ISender sender) =>
        {
            var command = new UpdateOrderCommand(
                request.RequestBody.OrderId,
                request.RequestBody.OrderData);

            var result = await sender.Send(command);
            var response = new UpdateOrderResponse(result.IsSuccess);

            return result.IsSuccess
                ? Results.Ok(response)
                : Results.NotFound(response);
        });
    }
}
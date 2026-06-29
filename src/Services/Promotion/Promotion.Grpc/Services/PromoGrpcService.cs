using Grpc.Core;
using MediatR;
using Promotion.Grpc.Protos;
using Promotion.Grpc.UseCases.CreatePromo;
using Promotion.Grpc.UseCases.DeletePromo;
using Promotion.Grpc.UseCases.GetPromo;
using Promotion.Grpc.UseCases.UpdatePromo;

namespace Promotion.Grpc.Services;

public class PromoGrpcService(IMediator mediator) : PromoService.PromoServiceBase
{
    public override async Task<PromoModel> GetPromo(GetPromoRequest request, ServerCallContext context)
    {
        var query = new GetPromoByCatalogItemIdQuery(request.CatalogItemId);
        var result = await mediator.Send(query);
        return result;
    }

    public override async Task<CreatePromoResponse> CreatePromo(CreatePromoRequest request, ServerCallContext context)
    {
        var command = new CreatePromoCommand(request);
        var result = await mediator.Send(command);
        return result;
    }

    public override async Task<UpdatePromoResponse> UpdatePromo(UpdatePromoRequest request, ServerCallContext context)
    {
        var command = new UpdatePromoCommand(request);
        var result = await mediator.Send(command);
        return result;
    }

    public override async Task<DeletePromoResponse> DeletePromo(DeletePromoRequest request, ServerCallContext context)
    {
        var command = new DeletePromoCommand(request);
        var result = await mediator.Send(command);
        return result;
    }
}
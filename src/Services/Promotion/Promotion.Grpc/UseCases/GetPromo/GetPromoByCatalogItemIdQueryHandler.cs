using Common.Kernel.CQRS.Queries;
using Grpc.Core;
using Mapster;
using Promotion.Grpc.Persistence.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.GetPromo;

public class GetPromoByCatalogItemIdQueryHandler(IPromoRepository promoRepository)
: IQueryHandler<GetPromoByCatalogItemIdQuery, PromoModel>
{
    public async Task<PromoModel> Handle(GetPromoByCatalogItemIdQuery request, CancellationToken cancellationToken)
    {
        var promo = await promoRepository.GetByCatalogItemIdAsync(request.CatalogItemId);

        if (promo is null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound,
                $"not fount {request.CatalogItemId}"));
        }
        
        var result = promo.Adapt<PromoModel>();
        return result;
    }
}
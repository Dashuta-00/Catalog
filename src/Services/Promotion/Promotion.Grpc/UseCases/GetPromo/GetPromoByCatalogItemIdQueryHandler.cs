using Common.Kernel.CQRS.Queries;
using Grpc.Core;
using Mapster;
using Promotion.Grpc.Persistence.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.GetPromo;

public class GetPromoByCatalogItemIdQueryHandler(IPromoRepository promoRepository)
: IQueryHandler<GetPromoByCatalogItemIdQuery, PromoModel>
{
    public async Task<PromoModel> Handle(GetPromoByCatalogItemIdQuery query, CancellationToken cancellationToken)
    {
        var promo = await promoRepository.GetByCatalogItemIdAsync(query.CatalogItemId);

        if (promo is null)
        {
            return new PromoModel
            {
                CatalogItemId = query.CatalogItemId,
                Value = 0
            };
        }
        
        var result = promo.Adapt<PromoModel>();
        return result;
    }
}
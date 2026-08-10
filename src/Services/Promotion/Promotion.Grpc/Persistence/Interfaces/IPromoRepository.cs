using Promotion.Grpc.Domain;

namespace Promotion.Grpc.Persistence.Interfaces;

public interface IPromoRepository
{
    Task<Promo?> GetByCatalogItemIdAsync(string catalogItemId, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Promo promo, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpdatePromoModel updatePromoModel, CancellationToken cancellationToken);
    Task<bool> DeleteByCatalogItemIdAsync(DeletePromoModel deletePromoModel, CancellationToken cancellationToken);
}
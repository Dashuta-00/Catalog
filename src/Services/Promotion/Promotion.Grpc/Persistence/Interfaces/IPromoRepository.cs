using Promotion.Grpc.Domain;

namespace Promotion.Grpc.Persistence.Interfaces;

public interface IPromoRepository
{
    Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId);
    Task<bool> CreateAsync(Promo? promo);
    
    Task<bool> UpdateAsync(UpdatePromoModel? updatePromoModel);
    Task<bool> DeleteByCatalogItemIdAsync(DeletePromoModel? deletePromoModel);
}
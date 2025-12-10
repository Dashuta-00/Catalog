using Catalog.Domain.Entities;
using Catalog.Domain.Specifications;

namespace Catalog.Domain.Repositories;

public interface ICatalogItemRepository
{
    Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken);
    Task<Pagination<CatalogItem>> GetAllCatalogItemsAsyncV2(QueryArgs args, CancellationToken cancellationToken);
    Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync(CancellationToken cancellationToken);
    Task<CatalogItem?> GetCatalogItemByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title, CancellationToken cancellationToken);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandTitleAsync(string brandTitle, CancellationToken cancellationToken);
    Task<bool> UpdateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken);
    Task<bool> DeleteCatalogItemAsync(Guid id, CancellationToken cancellationToken);
}
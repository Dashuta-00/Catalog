using Catalog.Domain.Entities;

namespace Catalog.Application.Responses.CatalogItemResponses;

public record GetCatalogItemsByBrandTitleResult(IEnumerable<CatalogItem> CatalogItems);

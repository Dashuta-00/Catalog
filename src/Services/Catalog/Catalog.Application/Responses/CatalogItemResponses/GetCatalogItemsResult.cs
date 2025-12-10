using Catalog.Domain.Entities;

namespace Catalog.Application.Responses.CatalogItemResponses;

public record GetCatalogItemsResult(IEnumerable<CatalogItem> CatalogItems);
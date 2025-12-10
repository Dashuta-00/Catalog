using Catalog.Domain.Entities;

namespace Catalog.Application.Responses.CatalogItemResponses;

public record GetCatalogItemByTitleResult(IEnumerable<CatalogItem> CatalogItems);
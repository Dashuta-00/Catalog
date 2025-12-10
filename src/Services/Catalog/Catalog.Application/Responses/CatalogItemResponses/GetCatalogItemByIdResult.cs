using Catalog.Domain.Entities;

namespace Catalog.Application.Responses.CatalogItemResponses;

public record GetCatalogItemByIdResult(CatalogItem? CatalogItem);
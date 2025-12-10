using Catalog.Domain.Entities;
using Catalog.Domain.Specifications;

namespace Catalog.Application.Responses.CatalogItemResponses;

public record GetCatalogItemsResultV2(Pagination<CatalogItem> CatalogItems);
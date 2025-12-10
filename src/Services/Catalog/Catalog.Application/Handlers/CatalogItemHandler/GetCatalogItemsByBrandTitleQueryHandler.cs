using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandler;

public class GetCatalogItemsByBrandTitleQueryHandler(ICatalogItemRepository catalogItemRepository) 
    : IRequestHandler<GetCatalogItemsByBrandTitleQuery, GetCatalogItemsByBrandTitleResult>
{
    public async Task<GetCatalogItemsByBrandTitleResult> Handle(GetCatalogItemsByBrandTitleQuery query, 
        CancellationToken cancellationToken)
    {
        var catalogItems =
            await catalogItemRepository.GetCatalogItemsByBrandTitleAsync(query.BrandTitle, cancellationToken);
        var result = new GetCatalogItemsByBrandTitleResult(catalogItems);
        return result;
    }
}
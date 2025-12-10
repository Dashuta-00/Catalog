using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandler;

public class GetCatalogItemsByTitleQueryHandler(ICatalogItemRepository catalogItemRepository) 
    : IRequestHandler<GetCatalogItemsByTitleQuery, GetCatalogItemByTitleResult>
{
    public async Task<GetCatalogItemByTitleResult> Handle(GetCatalogItemsByTitleQuery query, 
        CancellationToken cancellationToken)
    {
        var catalogItems = 
            await catalogItemRepository.GetCatalogItemsByTitleAsync(query.Title, cancellationToken);
        var result = new GetCatalogItemByTitleResult(catalogItems);
        return result;
    }
}
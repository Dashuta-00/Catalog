using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandler;

public class GetCatalogItemByIdQueryHandler(ICatalogItemRepository catalogItemRepository) : IRequestHandler<GetCatalogItemByIdQuery, GetCatalogItemByIdResult>
{
    public async Task<GetCatalogItemByIdResult> Handle(GetCatalogItemByIdQuery query, CancellationToken cancellationToken)
    {
        var catalogItem = await catalogItemRepository.GetCatalogItemByIdAsync(query.Id, cancellationToken);
        var result = new GetCatalogItemByIdResult(catalogItem);
        return result;
    }
}
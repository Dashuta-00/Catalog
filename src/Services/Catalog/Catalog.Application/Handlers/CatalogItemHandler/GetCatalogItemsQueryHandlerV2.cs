using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Specifications;
using Mapster;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandler;

public class GetCatalogItemsQueryHandlerV2(ICatalogItemRepository catalogItemRepository) : IRequestHandler<GetCatalogItemsQueryV2, GetCatalogItemsResultV2>
{
    public async Task<GetCatalogItemsResultV2> Handle(GetCatalogItemsQueryV2 query, CancellationToken cancellationToken)
    {
        var result = await catalogItemRepository.GetAllCatalogItemsAsyncV2(query.QueryArgs, cancellationToken);
        return new GetCatalogItemsResultV2(result);
    }
} 
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Specifications;
using MediatR;

namespace Catalog.Application.Queries.CatalogItemQueries;

public record GetCatalogItemsQueryV2(QueryArgs QueryArgs) : IRequest<GetCatalogItemsResultV2>;
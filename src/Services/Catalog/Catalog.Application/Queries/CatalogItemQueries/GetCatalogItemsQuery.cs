using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Queries.CatalogItemQueries;

public record GetCatalogItemsQuery : IRequest<GetCatalogItemsResult>;
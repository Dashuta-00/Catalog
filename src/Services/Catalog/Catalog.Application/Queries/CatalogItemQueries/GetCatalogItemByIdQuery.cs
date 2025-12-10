using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Queries.CatalogItemQueries;

public record GetCatalogItemByIdQuery(Guid Id) : IRequest<GetCatalogItemByIdResult>;
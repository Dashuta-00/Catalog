using Catalog.Application.Responses.CategoryResponses;
using MediatR;

namespace Catalog.Application.Queries.CategoryQueries;

public record GetCategoriesQuery : IRequest<GetCategoriesResult>;
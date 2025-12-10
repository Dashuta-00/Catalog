using Catalog.Application.Responses.BrandResponses;
using MediatR;

namespace Catalog.Application.Queries.BrandQueries;

public record GetBrandsQuery : IRequest<GetBrandsResult>;
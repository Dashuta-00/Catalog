using Catalog.Domain.Entities;

namespace Catalog.Application.Responses.BrandResponses;

public record GetBrandsResult(IEnumerable<Brand> Brands);
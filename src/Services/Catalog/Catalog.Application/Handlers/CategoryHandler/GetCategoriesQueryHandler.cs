using Catalog.Application.Queries.CategoryQueries;
using Catalog.Application.Responses.CategoryResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CategoryHandler;

public class GetCategoriesQueryHandler(ICategoryRepository  categoryRepository) : IRequestHandler<GetCategoriesQuery, GetCategoriesResult> 
{
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllCategoriesAsync(cancellationToken);
        var result = new GetCategoriesResult(categories);
        return result;
    }
}
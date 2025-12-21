using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Mapster;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandler;

public class CreateCatalogItemQueryHandler(
    ICatalogItemRepository catalogItemRepository,
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCatalogItemCommand, CreateCatalogItemResult>
{
    public async Task<CreateCatalogItemResult> Handle(CreateCatalogItemCommand command, 
        CancellationToken cancellationToken)
    {
        // Проверяем Brand
        var brandExists = command.Brand != null
            && (await brandRepository.GetAllBrandsAsync(cancellationToken))
            .Any(b => b.Id == command?.Brand?.Id);

        if (!brandExists)
        {
            throw new ArgumentException("Указанный бренд не существует");
        }

        // Проверяем Category
        var categoryExists = command.Category != null
            && ((await categoryRepository.GetAllCategoriesAsync(cancellationToken))
                .Any(c => c.Id == command.Category.Id));

        if (!categoryExists)
        {
            throw new ArgumentException("Указанная категория не существует");
        }

        var catalogItem = command.Adapt<CatalogItem>();
        catalogItem.Id = Guid.NewGuid();
        await catalogItemRepository.CreateCatalogItemAsync(catalogItem, cancellationToken);
        return new CreateCatalogItemResult(catalogItem.Id);
    }
}
using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Mapster;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandler;

public class UpdateCatalogItemQueryHandler(ICatalogItemRepository catalogItemRepository)
                : IRequestHandler<UpdateCatalogItemCommand, UpdateCatalogItemResult>
{
    public async Task<UpdateCatalogItemResult> Handle(UpdateCatalogItemCommand command, CancellationToken cancellationToken)
    {
        var existingItem = await catalogItemRepository.GetCatalogItemByIdAsync(command.Id, cancellationToken);
        if (existingItem is null)
        {
            return new UpdateCatalogItemResult(false);  
        }

        var catalogItem = command.Adapt<CatalogItem>();
        var isSuccess = await catalogItemRepository.UpdateCatalogItemAsync(catalogItem, cancellationToken);
        return new UpdateCatalogItemResult(isSuccess);
    }
}
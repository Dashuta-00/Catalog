using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandler;

public class DeleteCatalogItemHandler(ICatalogItemRepository catalogItemRepository) : IRequestHandler<DeleteCatalogItemCommand, DeleteCatalogItemResult>
{
    public async Task<DeleteCatalogItemResult> Handle(DeleteCatalogItemCommand command, CancellationToken cancellationToken)
    {
        var existingItem = await catalogItemRepository.GetCatalogItemByIdAsync(command.Id, cancellationToken);
        if (existingItem is null)
        {
            return new DeleteCatalogItemResult(false);
        }
        
        var result = await catalogItemRepository.DeleteCatalogItemAsync(command.Id, cancellationToken);
        return new DeleteCatalogItemResult(result);
    }
}
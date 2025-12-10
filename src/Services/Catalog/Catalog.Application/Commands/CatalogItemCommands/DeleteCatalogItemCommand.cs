using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Commands.CatalogItemCommands;

public record DeleteCatalogItemCommand(Guid Id) : IRequest<DeleteCatalogItemResult>;
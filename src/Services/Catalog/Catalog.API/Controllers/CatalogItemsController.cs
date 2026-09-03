using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/[controller]")] 
public class CatalogItemsController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCatalogItemsResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetCatalogItemsResult>> GetAllCatalogItems()
    {
        var result = await Mediator.Send(new GetCatalogItemsQuery());
        return Ok(result);
    }
    
    [HttpGet("{catalogItemId:guid}")]
    [ProducesResponseType(typeof(GetCatalogItemByIdResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetCatalogItemByIdResult>> GetById(Guid catalogItemId)
    {
        var result = await Mediator.Send(new GetCatalogItemByIdQuery(catalogItemId));
        return Ok(result);
    }
    
    [HttpGet("title/{catalogItemTitle}")]
    [ProducesResponseType(typeof(GetCatalogItemByTitleResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetCatalogItemByTitleResult>> GetByTitle(string catalogItemTitle)
    {
        var result = await Mediator.Send(new GetCatalogItemsByTitleQuery(catalogItemTitle));
        return Ok(result);
    }
    
    [HttpGet("brand/{brandTitle}")]
    [ProducesResponseType(typeof(GetCatalogItemsByBrandTitleResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetCatalogItemsByBrandTitleResult>> GetByBrandTitle(string brandTitle)
    {
        var result = await Mediator.Send(new GetCatalogItemsByBrandTitleQuery(brandTitle));
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CreateCatalogItemResult), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateCatalogItemResult>> CreateCatalogItem(
        [FromBody] CreateCatalogItemCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(
            nameof(CreateCatalogItem),
            new { id = result.Id },
            result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateCatalogItemResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<UpdateCatalogItemResult>> UpdateCatalogItem([FromBody] UpdateCatalogItemCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);    
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DeleteCatalogItemResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteCatalogItemResult>> DeleteCatalogItem(
        [FromBody] DeleteCatalogItemCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
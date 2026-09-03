using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiVersion("2")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v{version:apiVersion}/[controller]")] 
public class CatalogItemsControllerV2 : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCatalogItemsResultV2), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetCatalogItemsResultV2>> GetAllCatalogItems([FromQuery]QueryArgs? queryArgs)
    {
        var query = new GetCatalogItemsQueryV2(queryArgs!);
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}
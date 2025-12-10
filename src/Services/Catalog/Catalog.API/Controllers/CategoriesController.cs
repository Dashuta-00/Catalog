using System.Net;
using Catalog.Application.Queries.CategoryQueries;
using Catalog.Application.Responses.CategoryResponses;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class CategoriesController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCategoriesResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCategoriesResult>> GetAllCategories()
    {
        var categories = await Mediator.Send(new GetCategoriesQuery());
        return Ok(categories);
    }
}
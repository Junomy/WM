using MediatR;
using Microsoft.AspNetCore.Mvc;
using WM.Core.Application.Products.Commands.UpsertProducts;
using WM.Core.Application.Products.Queries.GetProduct;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var result = await _mediator.Send(new GetProductQuery { Id = id });
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertProductsCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WM.Core.Application.Products.Commands.DeleteProduct;
using WM.Core.Application.Products.Commands.UpdateProduct;
using WM.Core.Application.Products.Commands.UpsertProducts;
using WM.Core.Application.Products.Queries.GetProduct;
using WM.Core.Application.Products.Queries.GetProducts;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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

    [HttpPost("get")]
    public async Task<IActionResult> Get([FromBody] GetProductsQuery query)
    {
        var result = await _mediator.Send(query);
        if(result is null) { return NotFound(); }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        var result = await _mediator.Send(command);
        if(result == null) { return NotFound(); }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProductCommand { Id = id });
        if(result < 0) { return NotFound(); }
        return Ok(result);
    }
}

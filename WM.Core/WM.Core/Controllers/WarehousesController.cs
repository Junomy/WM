using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WM.Core.Application.Warehouses.Commands.UpsertWarehouses;
using WM.Core.Application.Warehouses.Queries.GetWarehouse;
using WM.Core.Application.Warehouses.Queries.GetWarehousesByFacility;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehousesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetWarehouseQuery { Id = id });
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("facility/{id}")]
    public async Task<IActionResult> GetByFacility(int id)
    {
        var result = await _mediator.Send(new GetWarehousesByFacilityIdQuery { FacilityId = id });
        if (result.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertWarehousesCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}

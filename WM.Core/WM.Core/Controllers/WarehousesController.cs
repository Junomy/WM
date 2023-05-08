using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WM.Core.Application.Warehouses.Commands.CreateWarehouse;
using WM.Core.Application.Warehouses.Commands.DeleteWarehouse;
using WM.Core.Application.Warehouses.Commands.UpdateWarehouse;
using WM.Core.Application.Warehouses.Queries.GetWarehouse;
using WM.Core.Application.Warehouses.Queries.GetWarehousesByFacility;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
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
    public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWarehouseCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteWarehouseCommand { Id = id });
        return Ok();
    }
}

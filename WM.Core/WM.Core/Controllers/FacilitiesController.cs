using MediatR;
using Microsoft.AspNetCore.Mvc;
using WM.Core.Application.Facilities.Commands.UpsertFacility;
using WM.Core.Application.Facilities.Queries.GetFacility;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacilitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FacilitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var result = await _mediator.Send(new GetFacilityQuery { Id = id });
        if(result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertFacilityCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}

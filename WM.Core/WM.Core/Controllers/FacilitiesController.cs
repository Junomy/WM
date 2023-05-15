using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WM.Core.Application.Facilities.Commands.UpsertFacility;
using WM.Core.Application.Facilities.Queries.GetFacilities;
using WM.Core.Application.Facilities.Queries.GetFacility;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetFaciltiiesCommand());
        if (result.IsNullOrEmpty())
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

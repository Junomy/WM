using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WM.Core.Application.Inventories.Commands.CreateInvItems;
using WM.Core.Application.Inventories.Commands.DeleteInvItem;
using WM.Core.Application.Inventories.Queries.GetFilterOptions;
using WM.Core.Application.Inventories.Queries.GetInventories;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("get")]
    public async Task<IActionResult> Get([FromBody] GetInventoriesQuery query)
    {
        var result = await _mediator.Send(query);

        if(result.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] CreateInvItemsCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet("filterOptions")]
    public async Task<IActionResult> GetFilterOptions()
    {
        return Ok(await _mediator.Send(new GetFilterOptionsQuery()));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteInvItemCommand { Id = id });
        if(result < 0)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}

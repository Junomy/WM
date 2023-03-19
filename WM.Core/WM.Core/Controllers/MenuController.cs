using MediatR;
using Microsoft.AspNetCore.Mvc;
using WM.Core.Application.Menu.Queries.GetMenu;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetMenuQuery()));
    }
}
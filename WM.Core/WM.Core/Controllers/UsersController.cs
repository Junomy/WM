using MediatR;
using Microsoft.AspNetCore.Mvc;
using WM.Core.Application.Products.Commands.UpsertProducts;
using WM.Core.Application.Products.Queries.GetProduct;
using WM.Core.Application.Users.Commands.Login;
using WM.Core.Application.Users.Commands.UpsertUser;
using WM.Core.Application.Users.Queries.GetUser;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var result = await _mediator.Send(new GetUserQuery { Id = id });
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        if(result is null)
        {
            return Unauthorized();
        }
        else return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertUserCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}

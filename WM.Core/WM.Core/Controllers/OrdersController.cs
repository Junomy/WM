﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WM.Core.Application.Orders.Commands.CreateOrder;
using WM.Core.Application.Orders.Commands.UpdateOrder;
using WM.Core.Application.Orders.Queries.GetOrder;
using WM.Core.Application.Orders.Queries.GetOrders;

namespace WM.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{facilityId}")]
    public async Task<IActionResult> Get(int? facilityId, [FromQuery] string? orderNumber, [FromQuery(Name = "statusIds")] int[]? statusIds)
    {
        var res = await _mediator.Send(new GetOrdersQuery {
            FacilityId = facilityId,
            OrderNumber = orderNumber,
            StatusIds = statusIds.ToList()
        });

        if(res.IsNullOrEmpty())
        {
            return NotFound(res);
        }

        return Ok(res);
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _mediator.Send(new GetOrderQuery { Id = id });

        if(res is null)
        {
            return NotFound(res);
        }

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Create([FromBody] UpdateOrderCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}

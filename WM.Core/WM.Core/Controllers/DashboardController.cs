using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WM.Core.Application.Dashboard.Queries.DonutWidget;
using WM.Core.Application.Dashboard.Queries.GetInfo;
using WM.Core.Application.Dashboard.Queries.LineChartWidget;

namespace WM.Core.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("donut/{facilityId}")]
    public async Task<IActionResult> GetDonutData(int facilityId)
    {
        return Ok(await _mediator.Send(new DonutWidgetQuery { FacilityId = facilityId }));
    }

    [HttpGet("line/{facilityId}")]
    public async Task<IActionResult> GetLineChartData(int facilityId)
    {
        return Ok(await _mediator.Send(new LineChartWidgetQuery { FacilityId = facilityId }));
    }

    [HttpGet("info/{facilityId}")]
    public async Task<IActionResult> GetInfoData(int facilityId)
    {
        return Ok(await _mediator.Send(new GetInfoQuery { FacilityId = facilityId }));
    }
}

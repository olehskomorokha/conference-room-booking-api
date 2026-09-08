using ConferenceRoomBooking.Service.Intefraces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("Room-utilization")]
    public async Task<IActionResult> GetRoomUtilizationAsync([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        return Ok(await _reportService.GetRoomUtilizationAsync(from, to));
    }

    [HttpGet("Revenue")]
    public async Task<IActionResult> GetRevenueAsync([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        return Ok(await _reportService.GetRevenueAsync(from, to));
    }

    [HttpGet("Room-profitability")]
    public async Task<IActionResult> GetRoomProfitabilityAsync([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        return Ok(await _reportService.GetRoomProfitabilityAsync(from, to));
    }
}
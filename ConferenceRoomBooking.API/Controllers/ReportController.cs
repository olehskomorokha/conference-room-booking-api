using ConferenceRoomBooking.Service.Intefraces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

/// <summary>Provides business reports for conference room bookings.</summary>
[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>Returns room utilization for the selected period.</summary>
    /// <param name="from">First date of the period, inclusive.</param>
    /// <param name="to">Last date of the period, inclusive.</param>
    /// <returns>Booked hours and utilization percentage for every conference room.</returns>
    [HttpGet("Room-utilization")]
    public async Task<IActionResult> GetRoomUtilizationAsync([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        return Ok(await _reportService.GetRoomUtilizationAsync(from, to));
    }

    /// <summary>Returns total and per-room revenue for the selected period.</summary>
    /// <param name="from">First date of the period, inclusive.</param>
    /// <param name="to">Last date of the period, inclusive.</param>
    /// <returns>Total revenue, average booking value, and revenue by room.</returns>
    [HttpGet("Revenue")]
    public async Task<IActionResult> GetRevenueAsync([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        return Ok(await _reportService.GetRevenueAsync(from, to));
    }

    /// <summary>Returns the profitability ranking of conference rooms for the selected period.</summary>
    /// <param name="from">First date of the period, inclusive.</param>
    /// <param name="to">Last date of the period, inclusive.</param>
    /// <returns>Rooms ordered by revenue, with booking count and average booking value.</returns>
    [HttpGet("Room-profitability")]
    public async Task<IActionResult> GetRoomProfitabilityAsync([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        return Ok(await _reportService.GetRoomProfitabilityAsync(from, to));
    }
}

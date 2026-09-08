namespace ConferenceRoomBooking.Service.Models.Report;

/// <summary>Revenue report for a selected period.</summary>
public class RevenueReportDto
{
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public int BookingCount { get; init; }
    public decimal TotalRevenue { get; init; }
    public decimal AverageBookingValue { get; init; }
    public List<RoomRevenueItemDto> Rooms { get; init; } = [];
}

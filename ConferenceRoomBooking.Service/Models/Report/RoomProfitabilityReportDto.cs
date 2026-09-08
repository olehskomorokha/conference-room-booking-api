namespace ConferenceRoomBooking.Service.Models.Report;

/// <summary>Conference room profitability ranking for a selected period.</summary>
public class RoomProfitabilityReportDto
{
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public List<RoomProfitabilityItemDto> Rooms { get; init; } = [];
}

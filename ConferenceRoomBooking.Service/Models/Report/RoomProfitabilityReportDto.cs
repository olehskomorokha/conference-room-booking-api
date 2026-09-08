namespace ConferenceRoomBooking.Service.Models.Report;

public class RoomProfitabilityReportDto
{
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public List<RoomProfitabilityItemDto> Rooms { get; init; } = [];
}
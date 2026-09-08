namespace ConferenceRoomBooking.Service.Models.Report;

public class RoomUtilizationReportDto
{
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public decimal TotalAvailableHoursPerRoom { get; init; }
    public List<RoomUtilizationItemDto> Rooms { get; init; } = [];
}
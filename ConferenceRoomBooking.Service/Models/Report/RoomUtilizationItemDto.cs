namespace ConferenceRoomBooking.Service.Models.Report;

public class RoomUtilizationItemDto
{
    public int ConferenceRoomId { get; init; }
    public string ConferenceRoomName { get; init; } = string.Empty;
    public int BookingCount { get; init; }
    public decimal BookedHours { get; init; }
    public decimal UtilizationPercent { get; init; }
}
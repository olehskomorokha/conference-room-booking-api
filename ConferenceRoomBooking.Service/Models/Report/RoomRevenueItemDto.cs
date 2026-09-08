namespace ConferenceRoomBooking.Service.Models.Report;

public class RoomRevenueItemDto
{
    public int ConferenceRoomId { get; init; }
    public string ConferenceRoomName { get; init; } = string.Empty;
    public int BookingCount { get; init; }
    public decimal Revenue { get; init; }
}
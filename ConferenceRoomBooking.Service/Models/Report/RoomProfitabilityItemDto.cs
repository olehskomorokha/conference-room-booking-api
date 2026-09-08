namespace ConferenceRoomBooking.Service.Models.Report;

public class RoomProfitabilityItemDto
{
    public int Rank { get; init; }
    public int ConferenceRoomId { get; init; }
    public string ConferenceRoomName { get; init; } = string.Empty;
    public int BookingCount { get; init; }
    public decimal Revenue { get; init; }
    public decimal AverageBookingValue { get; init; }
}
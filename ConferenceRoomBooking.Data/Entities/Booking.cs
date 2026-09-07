using ConferenceRoomBooking.Data.Enums;

namespace ConferenceRoomBooking.Data.Entities;

public class Booking
{
    public int Id { get; set; }
    public int ConferenceRoomId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public ConferenceRoom ConferenceRoom { get; set; } = null!;
}
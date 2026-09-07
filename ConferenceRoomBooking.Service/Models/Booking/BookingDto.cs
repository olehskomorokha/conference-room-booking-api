using ConferenceRoomBooking.Data.Enums;

namespace ConferenceRoomBooking.Service.Models.Booking;

public class BookingDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public ConferenceRoomDto ConferenceRoom { get; set; } = null!;
}
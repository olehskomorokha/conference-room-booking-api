using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Enums;

namespace ConferenceRoomBooking.Service.Models.Booking;

public class AddBookingDto
{
    public int ConferenceRoomId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
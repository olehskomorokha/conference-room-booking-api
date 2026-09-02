namespace ConferenceRoomBooking.Data.Entities;

public class ConferenceRoom
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }

    ICollection<RoomService>? RoomServices { get; set; }
    ICollection<Booking>? Bookings { get; set; }
}
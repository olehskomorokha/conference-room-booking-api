namespace ConferenceRoomBooking.Service.Models;

public class AddConferenceRoomDto
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }
    public List<int>? AdditionalServiceIds { get; set; }
}
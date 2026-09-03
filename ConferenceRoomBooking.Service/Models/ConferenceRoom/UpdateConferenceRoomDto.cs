namespace ConferenceRoomBooking.Service.Models;

public class UpdateConferenceRoomDto
{
    public string? Name { get; set; }
    public int? Capacity { get; set; }
    public decimal? BasePricePerHour { get; set; }
    public List<int>? AdditionalServicesIds { get; set; }
}
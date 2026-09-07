namespace ConferenceRoomBooking.Service.Models.Etc;

public class CalculatePriceModel
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<int>? AdditionalServiceIds { get; set; }
}
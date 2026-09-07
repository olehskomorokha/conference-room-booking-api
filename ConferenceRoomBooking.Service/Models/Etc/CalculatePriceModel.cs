namespace ConferenceRoomBooking.Service.Models.Etc;

public class CalculatePriceModel
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<int>? AdditionalServiceIds { get; set; }
}
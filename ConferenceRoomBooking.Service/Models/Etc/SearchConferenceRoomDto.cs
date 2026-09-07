namespace ConferenceRoomBooking.Service.Models.Etc;

public class SearchConferenceRoomDto
{
    public int Capacity { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
}
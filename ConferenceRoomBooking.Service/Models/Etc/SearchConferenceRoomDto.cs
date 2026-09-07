namespace ConferenceRoomBooking.Service.Models.Etc;

public class SearchConferenceRoomDto
{
    public int Capacity { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
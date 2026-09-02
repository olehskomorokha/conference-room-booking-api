namespace ConferenceRoomBooking.Data.Entities;

public class RoomService
{
    public int Id { get; set; }
    public int ConferenceRoomId { get; set; }
    public int AdditionalServiceId { get; set; }
    public ConferenceRoom ConferenceRoom { get; set; } 
    public AdditionalService AdditionalService { get; set; }
}
namespace ConferenceRoomBooking.Data.Entities;

public class AdditionalService
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public ICollection<RoomService> RoomServices { get; set; }
    
}
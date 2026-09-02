using ConferenceRoomBooking.Service.Models.AdditionalService;

namespace ConferenceRoomBooking.Service.Models;

public class ConferenceRoomDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }
    public List<AdditionalServiceDto> AdditionalServices { get; set; }
}
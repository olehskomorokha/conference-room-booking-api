using ConferenceRoomBooking.Service.Models.Etc;

namespace ConferenceRoomBooking.Service.Intefraces;

public interface IPaymentService
{
    public Task<decimal> CalculatePrice(int conferenceRoomId, CalculatePriceModel model);
}
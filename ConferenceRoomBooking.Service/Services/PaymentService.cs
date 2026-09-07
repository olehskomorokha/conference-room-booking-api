using ConferenceRoomBooking.Service.Exceptions;
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Models.Etc;

namespace ConferenceRoomBooking.Service.Services;

public class PaymentService : IPaymentService
{
    private readonly IConferenceRoomService _conferenceRoomService;

    public PaymentService(IConferenceRoomService conferenceRoomService)
    {
        _conferenceRoomService = conferenceRoomService;
    }

    public async Task<decimal> CalculatePrice(int conferenceRoomId, CalculatePriceModel model)
    {
        if (model.StartTime >= model.EndTime)
        {
            throw new PaymentException("Invalid_time", "Start time must be before end time");
        }

        decimal totalToPay = 0;
        var conferenceRoom = await _conferenceRoomService.GetByIdAsync(conferenceRoomId);

        var duration = model.EndTime - model.StartTime;
        // Calculating base price + duration
        totalToPay += conferenceRoom.BasePricePerHour * (int)duration.TotalHours;

        if (model.AdditionalServiceIds != null)
        {
            foreach (var service in conferenceRoom.AdditionalServices)
            {
                if (model.AdditionalServiceIds.Contains(service.Id))
                {
                    // add additional service price to totalToPay
                    totalToPay += service.Price;
                }
            }
        }

        return totalToPay;
    }
}
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
        ValidateTime(model.StartTime, model.EndTime);

        var conferenceRoom = await _conferenceRoomService.GetByIdAsync(conferenceRoomId);

        var totalToPay = CalculatePriceByTariffPeriod(
            model.StartTime,
            model.EndTime,
            conferenceRoom.BasePricePerHour);

        if (model.AdditionalServiceIds?.Count > 0)
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

        return decimal.Round(totalToPay, 2);
    }

    private static decimal CalculatePriceByTariffPeriod(
        TimeOnly startTime,
        TimeOnly endTime,
        decimal basePricePerHour)
    {
        var tariffPeriods = new[]
        {
            new TariffPeriod(
                new TimeOnly(6, 0),
                new TimeOnly(9, 0),
                0.90m),

            new TariffPeriod(
                new TimeOnly(9, 0),
                new TimeOnly(12, 0),
                1.00m),

            new TariffPeriod(
                new TimeOnly(12, 0),
                new TimeOnly(14, 0),
                1.15m),

            new TariffPeriod(
                new TimeOnly(14, 0),
                new TimeOnly(18, 0),
                1.00m),

            new TariffPeriod(
                new TimeOnly(18, 0),
                new TimeOnly(23, 0),
                0.80m)
        };

        decimal totalPrice = 0;

        foreach (var period in tariffPeriods)
        {
            var overlapStart = startTime > period.StartTime
                ? startTime
                : period.StartTime;

            var overlapEnd = endTime < period.EndTime
                ? endTime
                : period.EndTime;

            if (overlapStart >= overlapEnd)
            {
                continue;
            }

            var duration = overlapEnd - overlapStart;
            var hours = (decimal)duration.TotalMinutes / 60;

            totalPrice += hours *
                          basePricePerHour *
                          period.Multiplier;
        }

        return totalPrice;
    }

    private static void ValidateTime(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            throw new PaymentException(
                "Invalid_time",
                "Start time must be before end time.");
        }

        if (startTime < new TimeOnly(6, 0) ||
            endTime > new TimeOnly(23, 0))
        {
            throw new PaymentException(
                "Invalid_time",
                "Booking time must be between 06:00 and 23:00.");
        }
    }

    private record TariffPeriod(TimeOnly StartTime, TimeOnly EndTime, decimal Multiplier);
}
using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Interfaces;
using ConferenceRoomBooking.Service.Exceptions;
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Mappers;
using ConferenceRoomBooking.Service.Models.Report;

namespace ConferenceRoomBooking.Service.Services;

public class ReportService : IReportService
{
    private const int WorkingHoursPerDay = 17;
    private readonly IBookingRepository _bookingRepository;

    public ReportService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<RoomUtilizationReportDto> GetRoomUtilizationAsync(DateOnly from, DateOnly to)
    {
        var rooms = await GetRoomsAsync(from, to);
        var availableHours = (to.DayNumber - from.DayNumber + 1) * WorkingHoursPerDay;

        return ReportMapper.MapToRoomUtilizationReport(from, to, availableHours, rooms);
    }

    public async Task<RevenueReportDto> GetRevenueAsync(DateOnly from, DateOnly to)
    {
        var rooms = await GetRoomsAsync(from, to);
        return ReportMapper.MapToRevenueReport(from, to, rooms);
    }

    public async Task<RoomProfitabilityReportDto> GetRoomProfitabilityAsync(DateOnly from, DateOnly to)
    {
        var rooms = await GetRoomsAsync(from, to);
        return ReportMapper.MapToRoomProfitabilityReport(from, to, rooms);
    }

    private async Task<List<ConferenceRoom>> GetRoomsAsync(DateOnly from, DateOnly to)
    {
        if (from > to)
        {
            throw new ReportException("Invalid_period", "The start date cannot be later than the end date.");
        }

        return await _bookingRepository.GetRoomsWithBookingsAsync(from, to);
    }
}
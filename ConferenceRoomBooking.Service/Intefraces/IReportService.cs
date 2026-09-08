using ConferenceRoomBooking.Service.Models.Report;

namespace ConferenceRoomBooking.Service.Intefraces;

public interface IReportService
{
    Task<RoomUtilizationReportDto> GetRoomUtilizationAsync(DateOnly from, DateOnly to);
    Task<RevenueReportDto> GetRevenueAsync(DateOnly from, DateOnly to);
    Task<RoomProfitabilityReportDto> GetRoomProfitabilityAsync(DateOnly from, DateOnly to);
}
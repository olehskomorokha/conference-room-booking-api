using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Service.Models.Report;

namespace ConferenceRoomBooking.Service.Mappers;

public static class ReportMapper
{
    public static RoomUtilizationReportDto MapToRoomUtilizationReport(
        DateOnly from, DateOnly to, decimal availableHours, IEnumerable<ConferenceRoom> rooms)
    {
        return new RoomUtilizationReportDto
        {
            From = from,
            To = to,
            TotalAvailableHoursPerRoom = availableHours,
            Rooms = rooms
                .Select(room => MapToRoomUtilizationItem(room, availableHours))
                .OrderByDescending(item => item.UtilizationPercent)
                .ThenBy(item => item.ConferenceRoomName)
                .ToList()
        };
    }

    public static RevenueReportDto MapToRevenueReport(DateOnly from, DateOnly to, IEnumerable<ConferenceRoom> rooms)
    {
        var roomRevenue = rooms.Select(MapToRoomRevenueItem).ToList();
        var bookingCount = roomRevenue.Sum(item => item.BookingCount);
        var totalRevenue = roomRevenue.Sum(item => item.Revenue);

        return new RevenueReportDto
        {
            From = from,
            To = to,
            BookingCount = bookingCount,
            TotalRevenue = totalRevenue,
            AverageBookingValue = bookingCount == 0 ? 0 : Math.Round(totalRevenue / bookingCount, 2),
            Rooms = roomRevenue.OrderByDescending(item => item.Revenue).ThenBy(item => item.ConferenceRoomName).ToList()
        };
    }

    public static RoomProfitabilityReportDto MapToRoomProfitabilityReport(
        DateOnly from, DateOnly to, IEnumerable<ConferenceRoom> rooms)
    {
        var rankedRooms = rooms
            .Select(MapToRoomRevenueItem)
            .OrderByDescending(item => item.Revenue)
            .ThenByDescending(item => item.BookingCount)
            .ThenBy(item => item.ConferenceRoomName)
            .Select((item, index) => new RoomProfitabilityItemDto
            {
                Rank = index + 1,
                ConferenceRoomId = item.ConferenceRoomId,
                ConferenceRoomName = item.ConferenceRoomName,
                BookingCount = item.BookingCount,
                Revenue = item.Revenue,
                AverageBookingValue = item.BookingCount == 0 ? 0 : Math.Round(item.Revenue / item.BookingCount, 2)
            })
            .ToList();

        return new RoomProfitabilityReportDto { From = from, To = to, Rooms = rankedRooms };
    }

    private static RoomUtilizationItemDto MapToRoomUtilizationItem(ConferenceRoom room, decimal availableHours)
    {
        var bookedHours = room.Bookings?.Sum(booking => (decimal)(booking.EndTime - booking.StartTime).TotalHours) ?? 0;

        return new RoomUtilizationItemDto
        {
            ConferenceRoomId = room.Id,
            ConferenceRoomName = room.Name,
            BookingCount = room.Bookings?.Count ?? 0,
            BookedHours = bookedHours,
            UtilizationPercent = availableHours == 0 ? 0 : Math.Round(bookedHours / availableHours * 100, 2)
        };
    }

    private static RoomRevenueItemDto MapToRoomRevenueItem(ConferenceRoom room)
    {
        return new RoomRevenueItemDto
        {
            ConferenceRoomId = room.Id,
            ConferenceRoomName = room.Name,
            BookingCount = room.Bookings?.Count ?? 0,
            Revenue = room.Bookings?.Sum(booking => booking.TotalPrice) ?? 0
        };
    }
}
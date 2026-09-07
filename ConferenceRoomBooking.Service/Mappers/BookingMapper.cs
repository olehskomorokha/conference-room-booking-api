using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Enums;
using ConferenceRoomBooking.Service.Models.Booking;

namespace ConferenceRoomBooking.Service.Mappers;

public static class BookingMapper
{
    public static BookingDto MapToBookingDto(Booking booking)
    {
        return new BookingDto()
        {
            Id = booking.Id,
            UserName = booking.UserName,
            CreatedAt = booking.CreatedAt,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Date = booking.Date,
            Status = booking.Status,
            ConferenceRoom = ConferenceRoomMapper.ToConferenceRoomDto(booking.ConferenceRoom)
        };
    }

    public static Booking MapToAddBooking(AddBookingDto bookingDto)
    {
        return new Booking()
        {
            ConferenceRoomId = bookingDto.ConferenceRoomId,
            UserName = bookingDto.UserName,
            Date = bookingDto.Date,
            StartTime = bookingDto.StartTime,
            EndTime = bookingDto.EndTime,
            CreatedAt = DateTime.Now,
            Status = BookingStatus.Pending,
        };
    }
}
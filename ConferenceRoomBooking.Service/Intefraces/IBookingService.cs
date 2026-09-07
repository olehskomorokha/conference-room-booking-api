using ConferenceRoomBooking.Service.Models.Booking;

namespace ConferenceRoomBooking.Service.Intefraces;

public interface IBookingService
{
    public Task<List<BookingDto>> GetAllAsync();
    public Task AddAsync(AddBookingDto model);
}
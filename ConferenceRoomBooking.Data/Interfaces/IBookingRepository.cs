using ConferenceRoomBooking.Data.Entities;

namespace ConferenceRoomBooking.Data.Interfaces;

public interface IBookingRepository
{
    public Task<List<Booking>> GetAllAsync();
    public Task<List<ConferenceRoom>> GetRoomsWithBookingsAsync(DateOnly from, DateOnly to);
    public Task<bool> AddAsync(Booking model);
}

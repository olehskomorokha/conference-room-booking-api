using ConferenceRoomBooking.Data.Entities;

namespace ConferenceRoomBooking.Data.Interfaces;

public interface IBookingRepository
{
    public Task<List<Booking>> GetAllAsync();
    public Task AddAsync(Booking model);
}
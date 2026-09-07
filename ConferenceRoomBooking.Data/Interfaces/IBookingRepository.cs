using ConferenceRoomBooking.Data.Entities;

namespace ConferenceRoomBooking.Data.Interfaces;

public interface IBookingRepository
{
    public Task<List<Booking>> GetAllAsync();

    public Task<bool> HasOverlappingAsync(int conferenceRoomId, DateOnly date, TimeOnly startTime,
        TimeOnly endTime);

    public Task AddAsync(Booking model);
}
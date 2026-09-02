using ConferenceRoomBooking.Data.Entities;

namespace ConferenceRoomBooking.Data.Interfaces;

public interface IConferenceRoomRepository
{
    public Task<ConferenceRoom> GetByIdAsync(int id);
    public Task<int> AddAsync(ConferenceRoom conferenceRoom);
    public Task UpdateAsync(ConferenceRoom conferenceRoom);
    public Task DeleteAsync(ConferenceRoom conferenceRoom);
}
using ConferenceRoomBooking.Data.Entities;

namespace ConferenceRoomBooking.Data.Interfaces;

public interface IRoomServiceRepository
{
    public Task AddAsync(RoomService model);
    public Task AddRangeAsync(IEnumerable<RoomService> models);
    public Task DeleteAsync(RoomService model);
}
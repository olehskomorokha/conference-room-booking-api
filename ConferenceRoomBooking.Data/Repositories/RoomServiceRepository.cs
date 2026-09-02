using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Interfaces;

namespace ConferenceRoomBooking.Data.Repositories;

public class RoomServiceRepository : IRoomServiceRepository
{
    private readonly AppDbContext _appDbContext;

    public RoomServiceRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(RoomService conferenceRoom)
    {
        await _appDbContext.AddAsync(conferenceRoom);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<RoomService> conferenceRooms)
    {
        await _appDbContext.AddRangeAsync(conferenceRooms);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RoomService conferenceRoom)
    {
        _appDbContext.Remove(conferenceRoom);
        await _appDbContext.SaveChangesAsync();
    }
}
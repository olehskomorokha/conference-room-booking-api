using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Data.Repositories;

public class ConferenceRoomRepository : IConferenceRoomRepository
{
    private readonly AppDbContext _dbContext;

    public ConferenceRoomRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ConferenceRoom> GetByIdAsync(int id)
    {
        return await _dbContext.ConferenceRooms.Include(x => x.RoomServices)
            .ThenInclude(x => x.AdditionalService)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> AddAsync(ConferenceRoom conferenceRoom)
    {
        await _dbContext.ConferenceRooms.AddAsync(conferenceRoom);
        await _dbContext.SaveChangesAsync();
        return conferenceRoom.Id;
    }

    public async Task UpdateAsync(ConferenceRoom conferenceRoom)
    {
        _dbContext.ConferenceRooms.Update(conferenceRoom);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ConferenceRoom conferenceRoom)
    {
        _dbContext.ConferenceRooms.Remove(conferenceRoom);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ConferenceRoom>> GetAvailableAsync(int capacity, DateOnly date, TimeOnly startTime,
        TimeOnly endTime)
    {
        var conferenceRooms = await _dbContext.ConferenceRooms.Where(cr => cr.Capacity >= capacity)
            .Include(x => x.RoomServices!)
            .ThenInclude(x => x.AdditionalService)
            .Where(cr => !_dbContext.Bookings.Any(booking => booking.ConferenceRoomId == cr.Id &&
                                                             booking.Date == date &&
                                                             booking.StartTime < endTime &&
                                                             booking.EndTime > startTime))
            .ToListAsync();
        return conferenceRooms;
    }
}
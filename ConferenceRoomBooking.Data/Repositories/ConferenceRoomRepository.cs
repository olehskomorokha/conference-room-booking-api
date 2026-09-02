using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Interfaces;

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
        return await _dbContext.ConferenceRooms.FindAsync(id);
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
}
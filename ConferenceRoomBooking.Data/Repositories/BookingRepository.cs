using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Enums;
using ConferenceRoomBooking.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Data.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _dbContext;

    public BookingRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Booking>> GetAllAsync()
    {
        return await _dbContext.Bookings.Include(x => x.ConferenceRoom).ToListAsync();
    }

    public Task<bool> HasOverlappingAsync(int conferenceRoomId, DateOnly date, TimeOnly startTime,
        TimeOnly endTime)
    {
        return _dbContext.Bookings.AnyAsync(booking =>
            booking.ConferenceRoomId == conferenceRoomId &&
            booking.Date == date &&
            booking.StartTime < endTime &&
            booking.EndTime > startTime);
    }

    public async Task AddAsync(Booking model)
    {
        await _dbContext.Bookings.AddAsync(model);
        await _dbContext.SaveChangesAsync();
    }
}
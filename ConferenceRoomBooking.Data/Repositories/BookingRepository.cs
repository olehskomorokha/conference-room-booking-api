using System.Data;
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
        return await _dbContext.Bookings
            .Include(booking => booking.ConferenceRoom)
            .ToListAsync();
    }

    public async Task<List<ConferenceRoom>> GetRoomsWithBookingsAsync(DateOnly from, DateOnly to)
    {
        return await _dbContext.ConferenceRooms
            .AsNoTracking()
            .Include(room => room.Bookings!.Where(booking =>
                booking.Date >= from && booking.Date <= to &&
                booking.Status != BookingStatus.Cancelled))
            .ToListAsync();
    }

    public async Task<bool> AddAsync(Booking model)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var hasConflict = await _dbContext.Bookings.AnyAsync(booking =>
            booking.ConferenceRoomId == model.ConferenceRoomId &&
            booking.Date == model.Date &&
            booking.StartTime < model.EndTime &&
            booking.EndTime > model.StartTime);

        if (hasConflict)
        {
            await transaction.RollbackAsync();
            return false;
        }

        await _dbContext.Bookings.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        await transaction.CommitAsync();
        return true;
    }
}
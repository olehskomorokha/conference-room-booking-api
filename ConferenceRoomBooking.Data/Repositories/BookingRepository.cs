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
        return await _dbContext.Bookings.ToListAsync();
    }

    public async Task AddAsync(Booking model)
    {
        throw new NotImplementedException();
    }
}
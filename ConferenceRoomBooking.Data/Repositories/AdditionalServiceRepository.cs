using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Data.Repositories;

public class AdditionalServiceRepository : IAdditionalServiceRepository
{
    private readonly AppDbContext _dbContext;

    public AdditionalServiceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AdditionalService>> GetAllAsync()
    {
        return await _dbContext.AdditionalServices.ToListAsync();
    }

    public async Task<AdditionalService> GetByIdAsync(int id)
    {
        return await _dbContext.AdditionalServices.FindAsync(id);
    }

    public async Task AddAsync(AdditionalService model)
    {
        await _dbContext.AdditionalServices.AddAsync(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AdditionalService model)
    {
        _dbContext.AdditionalServices.Update(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AdditionalService model)
    {
        _dbContext.AdditionalServices.Remove(model);
        await _dbContext.SaveChangesAsync();
    }
}
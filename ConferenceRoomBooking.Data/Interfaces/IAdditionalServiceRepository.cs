using ConferenceRoomBooking.Data.Entities;

namespace ConferenceRoomBooking.Data.Interfaces;

public interface IAdditionalServiceRepository
{
    public Task<List<AdditionalService>> GetAllAsync();
    public Task<AdditionalService> GetByIdAsync(int id);
    public Task AddAsync(AdditionalService model);
    public Task UpdateAsync(AdditionalService model);
    public Task DeleteAsync(AdditionalService model);
}
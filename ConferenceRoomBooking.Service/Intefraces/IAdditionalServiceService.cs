using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Service.Models.AdditionalService;

namespace ConferenceRoomBooking.Service.Intefraces;

public interface IAdditionalServiceService
{
    public Task<List<AdditionalServiceDto>> GetAllAsync();
    public Task<AdditionalServiceDto> GetByIdAsync(int id);
    public Task AddAsync(AddAdditionalServiceDto model);
    public Task UpdateAsync(int id, UpdateAdditionalServiceDto model);
    public Task DeleteAsync(int id);
}
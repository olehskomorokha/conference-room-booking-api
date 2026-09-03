using ConferenceRoomBooking.Service.Models;

namespace ConferenceRoomBooking.Service.Intefraces;

public interface IConferenceRoomService
{
    public Task<ConferenceRoomDto> GetByIdAsync(int id);
    public Task<int> AddAsync(AddConferenceRoomDto model);
    public Task UpdateAsync(int id, UpdateConferenceRoomDto conferenceRoom);
    public Task DeleteAsync(int id);
    public Task DeleteRoomServicesAsync(int roomId, IReadOnlyCollection<int> serviceIds);
}
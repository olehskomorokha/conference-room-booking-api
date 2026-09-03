using ConferenceRoomBooking.Data.Interfaces;
using ConferenceRoomBooking.Service.Exceptions;
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Mappers;
using ConferenceRoomBooking.Service.Models;

namespace ConferenceRoomBooking.Service.Services;

public class ConferenceRoomService : IConferenceRoomService
{
    private readonly IConferenceRoomRepository _conferenceRoomRepository;
    private readonly IRoomServiceRepository _roomServiceRepository;

    public ConferenceRoomService(IConferenceRoomRepository conferenceRoomRepository,
        IRoomServiceRepository roomServiceRepository)
    {
        _conferenceRoomRepository = conferenceRoomRepository;
        _roomServiceRepository = roomServiceRepository;
    }

    public async Task<ConferenceRoomDto> GetByIdAsync(int id)
    {
        var conferenceRoom = await _conferenceRoomRepository.GetByIdAsync(id);
        if (conferenceRoom == null)
        {
            throw new ConferenceRoomException("Failed_to_Find", "Failed to Find Conference Room");
        }

        return ConferenceRoomMapper.ToConferenceRoomDto(conferenceRoom);
    }

    public async Task<int> AddAsync(AddConferenceRoomDto model)
    {
        if (model == null)
        {
            throw new ConferenceRoomException("Failed_to_Add", "Model is null");
        }

        var newConferenceRoomId =
            await _conferenceRoomRepository.AddAsync(ConferenceRoomMapper.ToAddConferenceRoom(model));

        if (model.AdditionalServiceIds != null && model.AdditionalServiceIds.Any())
        {
            await _roomServiceRepository.AddRangeAsync(
                RoomServiceMapper.MapToAddRoomServices(newConferenceRoomId, model.AdditionalServiceIds));
        }

        return newConferenceRoomId;
    }

    public async Task UpdateAsync(int id, UpdateConferenceRoomDto conferenceRoom)
    {
        var conferenceRoomToUpdate = await _conferenceRoomRepository.GetByIdAsync(id);
        await _conferenceRoomRepository.UpdateAsync(conferenceRoomToUpdate);
    }

    public async Task DeleteAsync(int id)
    {
        var conferenceRoomToDelete = await _conferenceRoomRepository.GetByIdAsync(id);
        await _conferenceRoomRepository.DeleteAsync(conferenceRoomToDelete);
    }
}
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
    private readonly IAdditionalServiceRepository _additionalServiceRepository;

    public ConferenceRoomService(IConferenceRoomRepository conferenceRoomRepository,
        IRoomServiceRepository roomServiceRepository, IAdditionalServiceRepository additionalServiceRepository)
    {
        _conferenceRoomRepository = conferenceRoomRepository;
        _roomServiceRepository = roomServiceRepository;
        _additionalServiceRepository = additionalServiceRepository;
    }

    public async Task<ConferenceRoomDto> GetByIdAsync(int id)
    {
        var conferenceRoom = await _conferenceRoomRepository.GetByIdAsync(id);
        if (conferenceRoom == null)
        {
            throw new ConferenceRoomException("Failed_to_Add", $"Failed to Find Conference Room with Id = {id}");
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
        if (conferenceRoomToUpdate == null)
        {
            throw new ConferenceRoomException("Failed_to_Update", $"Failed to Find Conference Room with Id = {id}");
        }
    
        if (conferenceRoom.Name != null)
        {
            conferenceRoomToUpdate.Name = conferenceRoom.Name;
        }

        if (conferenceRoom.BasePricePerHour != null)
        {
            conferenceRoomToUpdate.BasePricePerHour = conferenceRoom.BasePricePerHour.Value;
        }

        if (conferenceRoom.Capacity != null)
        {
            conferenceRoomToUpdate.Capacity = conferenceRoom.Capacity.Value;
        }

        if (conferenceRoom.AdditionalServicesIds != null)
        {
            var existingServiceIds = conferenceRoomToUpdate.RoomServices?
                .Select(rs => rs.AdditionalServiceId)
                .ToHashSet();
            var newServiceIds = conferenceRoom.AdditionalServicesIds
                .Distinct()
                .Where(serviceId => existingServiceIds != null && !existingServiceIds.Contains(serviceId))
                .ToList();
            foreach (var newServiceId in newServiceIds)
            {
                var additionalService = await _additionalServiceRepository.GetByIdAsync(newServiceId);
                if (additionalService == null)
                {
                    throw new ConferenceRoomException("Failed_to_Update_Service",
                        $"Service with id {newServiceId} Not Found");
                }
            }

            if (newServiceIds.Count() > 0)
            {
                await _roomServiceRepository.AddRangeAsync(RoomServiceMapper.MapToAddRoomServices(id, newServiceIds));
            }
        }

        await _conferenceRoomRepository.UpdateAsync(conferenceRoomToUpdate);
    }

    public async Task DeleteAsync(int id)
    {
        var conferenceRoomToDelete = await _conferenceRoomRepository.GetByIdAsync(id);
        if (conferenceRoomToDelete == null)
        {
            throw new ConferenceRoomException("Failed_to_Delete", $"Failed to Find Conference Room with Id = {id}");
        }

        if (conferenceRoomToDelete.RoomServices != null)
        {
            foreach (var conferenceRoomService in conferenceRoomToDelete.RoomServices)
            {
                await _roomServiceRepository.DeleteAsync(conferenceRoomService);
            }
        }

        await _conferenceRoomRepository.DeleteAsync(conferenceRoomToDelete);
    }
}
using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Interfaces;

namespace ConferenceRoomBooking.Service.Mappers;

public static class RoomServiceMapper
{
    public static RoomService MapToAddRoomService(int conferenceRoomId, int additionalServiceId)
    {
        return new RoomService()
        {
            ConferenceRoomId = conferenceRoomId,
            AdditionalServiceId = additionalServiceId
        };
    }

    public static IEnumerable<RoomService> MapToAddRoomServices(int conferenceRoomId, List<int> additionalServiceIds)
    {
        return additionalServiceIds.Select(additionalServiceId => new RoomService()
        {
            ConferenceRoomId = conferenceRoomId,
            AdditionalServiceId = additionalServiceId
        });
    }
}
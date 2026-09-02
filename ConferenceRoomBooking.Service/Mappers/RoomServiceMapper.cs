using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Data.Interfaces;

namespace ConferenceRoomBooking.Service.Mappers;

public static class RoomServiceMapper
{
    public static RoomService ToAddRoomService(int conferenceRoomId, int additionalServiceId)
    {
        return new RoomService()
        {
            ConferenceRoomId = conferenceRoomId,
            AdditionalServiceId = additionalServiceId
        };
    }
}
using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Service.Models;

namespace ConferenceRoomBooking.Service.Mappers;

public static class ConferenceRoomMapper
{
    public static ConferenceRoomDto ToConferenceRoomDto(ConferenceRoom conferenceRoom)
    {
        return new ConferenceRoomDto
        {
            Id = conferenceRoom.Id,
            Capacity = conferenceRoom.Capacity,
            BasePricePerHour = conferenceRoom.BasePricePerHour,
            Name = conferenceRoom.Name
        };
    }

    public static ConferenceRoom ToAddConferenceRoom(AddConferenceRoomDto conferenceRoomDto)
    {
        return new ConferenceRoom()
        {
            Name = conferenceRoomDto.Name,
            Capacity = conferenceRoomDto.Capacity,
            BasePricePerHour = conferenceRoomDto.BasePricePerHour
        };
    }
}
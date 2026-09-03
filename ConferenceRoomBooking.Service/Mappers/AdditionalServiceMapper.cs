using ConferenceRoomBooking.Data.Entities;
using ConferenceRoomBooking.Service.Models.AdditionalService;

namespace ConferenceRoomBooking.Service.Mappers;

public static class AdditionalServiceMapper
{
    public static AdditionalServiceDto MapToAdditionalServiceDto(AdditionalService additionalService)
    {
        return new AdditionalServiceDto()
        {
            Id = additionalService.Id,
            Name = additionalService.Name,
            Price = additionalService.Price
        };
    }

    public static AdditionalService MapToAddAdditionalService(AddAdditionalServiceDto addAdditionalServiceDto)
    {
        return new AdditionalService()
        {
            Name = addAdditionalServiceDto.Name,
            Price = addAdditionalServiceDto.Price,
        };
    }
}
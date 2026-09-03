using ConferenceRoomBooking.Data.Interfaces;
using ConferenceRoomBooking.Service.Exceptions;
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Mappers;
using ConferenceRoomBooking.Service.Models.AdditionalService;

namespace ConferenceRoomBooking.Service.Services;

public class AdditionalServiceService : IAdditionalServiceService
{
    private readonly IAdditionalServiceRepository _additionalServiceRepository;

    public AdditionalServiceService(IAdditionalServiceRepository additionalServiceRepository)
    {
        _additionalServiceRepository = additionalServiceRepository;
    }


    public async Task<List<AdditionalServiceDto>> GetAllAsync()
    {
        var additionalServices = await _additionalServiceRepository.GetAllAsync();
        return additionalServices.Select(AdditionalServiceMapper.MapToAdditionalServiceDto).ToList();
    }

    public async Task<AdditionalServiceDto> GetByIdAsync(int id)
    {
        var  additionalService = await _additionalServiceRepository.GetByIdAsync(id);
        if (additionalService == null)
        {
            throw new AdditionalServiceException("Failed_to_Find", "Cant find this model");
        }
        return AdditionalServiceMapper.MapToAdditionalServiceDto(additionalService);
    }

    public async Task AddAsync(AddAdditionalServiceDto model)
    {
        if (model == null)
        {
            throw new AdditionalServiceException("Failed_to_Add", "Model is null");
        }

        await _additionalServiceRepository.AddAsync(AdditionalServiceMapper.MapToAddAdditionalService(model));
    }

    public async Task UpdateAsync(int id, UpdateAdditionalServiceDto model)
    {
        var modelToUpdate = await _additionalServiceRepository.GetByIdAsync(id);
        if (modelToUpdate == null)
        {
            throw new AdditionalServiceException("Failed_to_Update", "Cant find this model");
        }

        if (model.Name != null)
        {
            modelToUpdate.Name = model.Name;
        }

        if (model.Price != null)
        {
            if (model.Price <= 0)
            {
                throw new AdditionalServiceException("Failed_to_Update_Price", "Price must be greater than 0");
            }

            modelToUpdate.Price = model.Price.Value;
        }

        await _additionalServiceRepository.UpdateAsync(modelToUpdate);
    }

    public async Task DeleteAsync(int id)
    {
        var modelToDelete = await _additionalServiceRepository.GetByIdAsync(id);
        if (modelToDelete == null)
        {
            throw new AdditionalServiceException("Failed_to_Delete", "Cant find this model");
        }

        await _additionalServiceRepository.DeleteAsync(modelToDelete);
    }
}
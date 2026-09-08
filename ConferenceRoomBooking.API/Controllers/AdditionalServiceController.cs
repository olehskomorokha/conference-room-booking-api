using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Models.AdditionalService;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

/// <summary>Manages additional services available for conference rooms.</summary>
[ApiController]
[Route("api/[controller]")]
public class AdditionalServiceController : ControllerBase
{
    private readonly IAdditionalServiceService _additionalServiceService;

    public AdditionalServiceController(IAdditionalServiceService additionalServiceService)
    {
        _additionalServiceService = additionalServiceService;
    }

    /// <summary>Returns all additional services.</summary>
    /// <returns>A list of additional services.</returns>
    [HttpGet]
    public async Task<List<AdditionalServiceDto>> GetAllAsync()
    {
        return await _additionalServiceService.GetAllAsync();
    }

    /// <summary>Returns an additional service by identifier.</summary>
    /// <param name="id">Additional service identifier.</param>
    /// <returns>The requested additional service.</returns>
    [HttpGet("{id}")]
    public async Task<AdditionalServiceDto> GetByIdAsync(int id)
    {
        return await _additionalServiceService.GetByIdAsync(id);
    }

    /// <summary>Creates an additional service.</summary>
    /// <param name="model">New additional service details.</param>
    [HttpPost]
    public async Task AddAsync(AddAdditionalServiceDto model)
    {
        await _additionalServiceService.AddAsync(model);
    }

    /// <summary>Updates an additional service.</summary>
    /// <param name="id">Additional service identifier.</param>
    /// <param name="model">Updated additional service details.</param>
    [HttpPut("{id}")]
    public async Task AddAsync(int id, UpdateAdditionalServiceDto model)
    {
        await _additionalServiceService.UpdateAsync(id, model);
    }

    /// <summary>Deletes an additional service.</summary>
    /// <param name="id">Additional service identifier.</param>
    [HttpDelete("{id}")]
    public async Task DeleteAsync(int id)
    {
        await _additionalServiceService.DeleteAsync(id);
    }
}

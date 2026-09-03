using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Models.AdditionalService;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdditionalServiceController : ControllerBase
{
    private readonly IAdditionalServiceService _additionalServiceService;

    public AdditionalServiceController(IAdditionalServiceService additionalServiceService)
    {
        _additionalServiceService = additionalServiceService;
    }

    [HttpGet]
    public async Task<List<AdditionalServiceDto>> GetAllAsync()
    {
        return await _additionalServiceService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<AdditionalServiceDto> GetByIdAsync(int id)
    {
        return await _additionalServiceService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddAsync(AddAdditionalServiceDto model)
    {
        await _additionalServiceService.AddAsync(model);
    }

    [HttpPut("{id}")]
    public async Task AddAsync(int id, UpdateAdditionalServiceDto model)
    {
        await _additionalServiceService.UpdateAsync(id, model);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(int id)
    {
        await _additionalServiceService.DeleteAsync(id);
    }
}
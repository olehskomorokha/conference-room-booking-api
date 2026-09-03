using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConferenceRoomController : ControllerBase
{
    private readonly IConferenceRoomService _conferenceRoomService;

    public ConferenceRoomController(IConferenceRoomService conferenceRoomService)
    {
        _conferenceRoomService = conferenceRoomService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _conferenceRoomService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddConferenceRoomDto model)
    {
        return Ok(await _conferenceRoomService.AddAsync(model));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateConferenceRoomDto model)
    {
        await _conferenceRoomService.UpdateAsync(id, model);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _conferenceRoomService.DeleteAsync(id);
        return Ok();
    }
}
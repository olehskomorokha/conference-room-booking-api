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

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddConferenceRoomDto model)
    {
        return Ok(await _conferenceRoomService.AddAsync(model));
    }
}
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Models;
using ConferenceRoomBooking.Service.Models.Etc;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

/// <summary>Manages conference rooms and their additional services.</summary>
[ApiController]
[Route("api/[controller]")]
public class ConferenceRoomController : ControllerBase
{
    private readonly IConferenceRoomService _conferenceRoomService;

    public ConferenceRoomController(IConferenceRoomService conferenceRoomService)
    {
        _conferenceRoomService = conferenceRoomService;
    }

    /// <summary>Returns a conference room by identifier.</summary>
    /// <param name="id">Conference room identifier.</param>
    /// <returns>The requested conference room.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _conferenceRoomService.GetByIdAsync(id));
    }

    /// <summary>Creates a conference room.</summary>
    /// <param name="model">New conference room details.</param>
    /// <returns>Identifier of the created conference room.</returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync(AddConferenceRoomDto model)
    {
        return Ok(await _conferenceRoomService.AddAsync(model));
    }

    /// <summary>Updates a conference room.</summary>
    /// <param name="id">Conference room identifier.</param>
    /// <param name="model">Updated conference room details.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateConferenceRoomDto model)
    {
        await _conferenceRoomService.UpdateAsync(id, model);
        return Ok();
    }

    /// <summary>Deletes a conference room.</summary>
    /// <param name="id">Conference room identifier.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _conferenceRoomService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>Removes additional services from a conference room.</summary>
    /// <param name="conferenceRoomId">Conference room identifier.</param>
    /// <param name="serviceIds">Identifiers of services to remove.</param>
    [HttpDelete("Service/{conferenceRoomId}")]
    public async Task<IActionResult> DeleteServiceAsync(int conferenceRoomId,
        [FromBody] IReadOnlyCollection<int> serviceIds)
    {
        await _conferenceRoomService.DeleteRoomServicesAsync(conferenceRoomId, serviceIds);
        return NoContent();
    }

    /// <summary>Finds conference rooms available for the requested time and capacity.</summary>
    /// <param name="searchConferenceRoomDto">Required capacity, date and time interval.</param>
    /// <returns>Available conference rooms.</returns>
    [HttpPost("GetAvailable")]
    public async Task<IActionResult> GetAvailableAsync(SearchConferenceRoomDto searchConferenceRoomDto)
    {
        return Ok(await _conferenceRoomService.GetAvailableAsync(searchConferenceRoomDto));
    }
}

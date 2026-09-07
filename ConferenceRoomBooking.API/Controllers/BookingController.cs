using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Exceptions;
using ConferenceRoomBooking.Service.Models.Booking;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _bookingService.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddBookingDto bookingDto)
    {
        await _bookingService.AddAsync(bookingDto);
        return Ok();
    }
}
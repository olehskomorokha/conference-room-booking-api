using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Exceptions;
using ConferenceRoomBooking.Service.Models.Booking;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

/// <summary>Manages conference room bookings.</summary>
[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>Returns all bookings.</summary>
    /// <returns>A list of bookings with conference room details.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _bookingService.GetAllAsync());
    }

    /// <summary>Creates a booking and returns its calculated price.</summary>
    /// <param name="bookingDto">Booking details, including room, date, time and optional services.</param>
    /// <returns>Total booking price.</returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddBookingDto bookingDto)
    {
        return Ok(await _bookingService.AddAsync(bookingDto));
    }
}

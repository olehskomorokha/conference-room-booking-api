using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Models.Etc;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

/// <summary>Calculates booking prices.</summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    /// <summary>Calculates the price of booking a conference room.</summary>
    /// <param name="conferenceRoomId">Conference room identifier.</param>
    /// <param name="model">Booking time and selected additional services.</param>
    /// <returns>The calculated booking price.</returns>
    [HttpPost("Calculate-price")]
    public async Task<IActionResult> GetPrice([FromQuery] int conferenceRoomId, [FromBody] CalculatePriceModel model)
    {
        return Ok(await _paymentService.CalculatePrice(conferenceRoomId, model));
    }
}

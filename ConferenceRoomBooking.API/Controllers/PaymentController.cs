using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Models.Etc;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("Сalculate-price")]
    public async Task<IActionResult> GetPrice([FromQuery] int conferenceRoomId, [FromBody] CalculatePriceModel model)
    {
        return Ok(await _paymentService.CalculatePrice(conferenceRoomId, model));
    }
}
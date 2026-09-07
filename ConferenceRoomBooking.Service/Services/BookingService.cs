using ConferenceRoomBooking.Data.Interfaces;
using ConferenceRoomBooking.Service.Exceptions;
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Mappers;
using ConferenceRoomBooking.Service.Models.Booking;
using ConferenceRoomBooking.Service.Models.Etc;

namespace ConferenceRoomBooking.Service.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPaymentService _paymentService;

    public BookingService(IBookingRepository bookingRepository, IPaymentService paymentService)
    {
        _bookingRepository = bookingRepository;
        _paymentService = paymentService;
    }

    public async Task<List<BookingDto>> GetAllAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Select(BookingMapper.MapToBookingDto).ToList();
    }

    public async Task AddAsync(AddBookingDto model)
    {
        if (model == null)
        {
            throw new BookingException("Failed_to_Add", "Model is null");
        }

        if (model.StartTime >= model.EndTime)
        {
            throw new BookingException("Invalid_booking_time", "The end time must be later than the start time.");
        }

        if (await _bookingRepository.HasOverlappingAsync(model.ConferenceRoomId, model.Date, model.StartTime, model.EndTime))
        {
            throw new BookingException("Room_unavailable", "The room is already booked for the selected time.");
        }
        var totalPrice = await _paymentService.CalculatePrice(model.ConferenceRoomId, new CalculatePriceModel()
        {
            AdditionalServiceIds =  model.AdditionalServiceIds,
            StartTime = model.StartTime,
            EndTime = model.EndTime
        });

        var bookingToAdd = BookingMapper.MapToAddBooking(model);
        bookingToAdd.TotalPrice = totalPrice;
        
        await _bookingRepository.AddAsync(bookingToAdd);
    }
}
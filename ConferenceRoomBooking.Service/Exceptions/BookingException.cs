namespace ConferenceRoomBooking.Service.Exceptions;

public class BookingException : SystemException
{
    public BookingException(string code, string message)
        : base(code, message)
    {
    }
}
namespace ConferenceRoomBooking.Service.Exceptions;

public class PaymentException : SystemException
{
    public PaymentException(string code, string message)
        : base(code, message)
    {
    }
}
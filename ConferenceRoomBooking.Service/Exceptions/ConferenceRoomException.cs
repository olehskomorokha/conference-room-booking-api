namespace ConferenceRoomBooking.Service.Exceptions;

public class ConferenceRoomException : SystemException
{
    public ConferenceRoomException(string code, string message)
        : base(code, message)
    {
    }
}
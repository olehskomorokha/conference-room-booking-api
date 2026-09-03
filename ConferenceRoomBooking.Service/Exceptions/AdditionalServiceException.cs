namespace ConferenceRoomBooking.Service.Exceptions;

public class AdditionalServiceException : SystemException
{
    public AdditionalServiceException(string code, string message)
        : base(code, message)
    {
    }
}
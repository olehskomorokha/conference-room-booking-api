namespace ConferenceRoomBooking.Service.Exceptions;

public class ReportException : SystemException
{
    public ReportException(string code, string message)
        : base(code, message)
    {
    }
}
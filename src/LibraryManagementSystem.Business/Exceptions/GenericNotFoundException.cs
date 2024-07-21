using System.Net;

namespace LibraryManagementSystem.Business.Exceptions;

public class GenericNotFoundException : Exception, IBaseException
{
    public GenericNotFoundException(string message) : base(message)
    {
        Message = message;
    }

    public int StatusCode { get; set; } = (int)HttpStatusCode.NotFound;
    public string Message { get; set; }
}

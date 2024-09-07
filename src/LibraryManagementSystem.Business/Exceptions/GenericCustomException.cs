using System.Net;

namespace LibraryManagementSystem.Business.Exceptions;

public class GenericCustomException : Exception, IBaseException
{
    public GenericCustomException(string message, int code) : base(message)
    {
        Message = message;
        StatusCode = code;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
}


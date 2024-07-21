using LibraryManagementSystem.Business.Exceptions;
using System.Net;

namespace LibraryManagementSystem.Business.Exceptionsı
{
    public class GenericBadRequestException : Exception, IBaseException
    {
        public GenericBadRequestException(string message) : base(message)
        {
            Message = message;
        }

        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string Message { get; set; }
    }
}

using System.Net;

namespace LibraryManagementSystem.Business.Exceptions
{
    public class GenericIsExistException : Exception, IBaseException
    {
        public GenericIsExistException(string message) : base(message)
        {
            Message = message;
        }

        public int StatusCode { get; set; } = (int)HttpStatusCode.Conflict;
        public string Message { get; set; }
    }
}

namespace LibraryManagementSystem.Business.DTOs;

public class GenericResponseDto
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public GenericResponseDto(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}

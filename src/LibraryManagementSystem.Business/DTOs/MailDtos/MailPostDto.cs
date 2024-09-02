using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Business.DTOs.MailDtos;

public class MailPostDto
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<IFormFile> Attachments { get; set; }
}

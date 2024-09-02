
using LibraryManagementSystem.Business.DTOs.MailDtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace LibraryManagementSystem.Business.Utils.Helpers;

internal class EmailHelper
{
    public async Task SendEmailAsync(MailPostDto mailPostDto)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(Constants.mail);
        email.To.Add(MailboxAddress.Parse(mailPostDto.ToEmail));
        email.Subject = mailPostDto.Subject;
        var builder = new BodyBuilder();
        if (mailPostDto.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in mailPostDto.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = mailPostDto.Body;
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(Constants.host, Constants.port, SecureSocketOptions.StartTls);
        smtp.Authenticate(Constants.mail, Constants.password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}

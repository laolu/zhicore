using Microsoft.Extensions.Options;
using System.Net.Mail;
using ZhiCore.Application.Common.Interfaces;
using ZhiCore.Application.Common.Models;

namespace ZhiCore.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
    {
        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
        {
            EnableSsl = _emailSettings.EnableSsl,
            Credentials = new System.Net.NetworkCredential(_emailSettings.UserName, _emailSettings.Password)
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }

    public async Task SendEmailAsync(string to, string subject, string body, string[] attachments, bool isHtml = false)
    {
        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
        {
            EnableSsl = _emailSettings.EnableSsl,
            Credentials = new System.Net.NetworkCredential(_emailSettings.UserName, _emailSettings.Password)
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        mailMessage.To.Add(to);

        if (attachments != null)
        {
            foreach (var attachment in attachments)
            {
                if (File.Exists(attachment))
                {
                    mailMessage.Attachments.Add(new Attachment(attachment));
                }
            }
        }

        await client.SendMailAsync(mailMessage);
    }
}
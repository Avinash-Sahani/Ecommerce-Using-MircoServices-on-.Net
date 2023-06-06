using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail;

public class EmailService : IEmailService
{
    public EmailSettings _EmailSettings { get; }
    
    public ILogger<EmailService> _logger { get; }

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _EmailSettings = emailSettings.Value;
        _logger = logger;
    }

    public  async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_EmailSettings.ApiKey);
        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;
        var from = new EmailAddress(_EmailSettings.FromAddress, _EmailSettings.FromName);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        _logger.LogInformation("Email Sending Intialized;");
        var response = await client.SendEmailAsync(msg);

        if (response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK)
        {
            _logger.LogInformation("The email has been sent succesfully");
            return true;
        }

        _logger.LogError($"Error occured in sending email {response.Body} ");
        return false;
    }
}
using System.Net;
using System.Net.Mail;
using dotnetWebApiCoreCBA.Common;
using dotnetWebApiCoreCBA.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace dotnetWebApiCoreCBA.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailOptions, ILogger<EmailService> logger)
    {
        _settings = emailOptions.Value;
        _logger = logger;
    }

    public async Task SendAsync(
        string toEmail,
        string subject,
        string body,
        bool isHtml = true,
        CancellationToken cancellationToken = default)
    {
        // In early dev, you can start with just logging:
        if (_settings.Provider == "Mock")
        {
            _logger.LogInformation("Mock Email → To: {To}, Subject: {Subject}, Body: {Body}",
                toEmail, subject, body);
            return;
        }
        // ========= VALIDATE EMAILS START =========
        if (!MailAddress.TryCreate(toEmail, out var toAddress))
        {
            _logger.LogWarning("EmailService: Invalid TO email: {Email}", toEmail);
            return; // swallow error
        }

        if (!MailAddress.TryCreate(_settings.FromAddress, out var fromAddress))
        {
            _logger.LogError("EmailService: Invalid FROM email in config: {Email}", _settings.FromAddress);
            return;
        }
        // ========= VALIDATE EMAILS END =========

        if (_settings.Provider == "Smtp")
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromAddress, _settings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            message.To.Add(new MailAddress(toEmail));

            using var client = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPassword)
            };

            _logger.LogInformation("Sending SMTP email to {To}", toEmail);
            await client.SendMailAsync(message, cancellationToken);
        }
        else
        {
            _logger.LogWarning("Unknown email provider: {Provider}", _settings.Provider);
        }
    }
}

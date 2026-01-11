namespace dotnetWebApiCoreCBA.Common;

public class EmailSettings
{
    public string FromName { get; set; } = string.Empty;
    public string FromAddress { get; set; } = string.Empty;

    // For SMTP
    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;

    // For future: SendGrid or others
    public string Provider { get; set; } = "Smtp"; // or "SendGrid" etc.
}

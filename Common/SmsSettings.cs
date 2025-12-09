namespace dotnetWebApiCoreCBA.Common;

public class SmsSettings
{
    public string Provider { get; set; } = "Mock"; // Twilio, etc.
    public string ApiKey { get; set; } = string.Empty;
    public string SenderId { get; set; } = string.Empty;
}

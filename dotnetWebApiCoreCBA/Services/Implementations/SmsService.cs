using dotnetWebApiCoreCBA.Common;
using dotnetWebApiCoreCBA.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dotnetWebApiCoreCBA.Services.Implementations;

public class SmsService : ISmsService
{
    private readonly SmsSettings _settings;
    private readonly ILogger<SmsService> _logger;

    public SmsService(IOptions<SmsSettings> smsOptions, ILogger<SmsService> logger)
    {
        _settings = smsOptions.Value;
        _logger = logger;
    }

    public Task SendAsync(
        string toPhoneNumber,
        string message,
        CancellationToken cancellationToken = default)
    {
        // For now just log; later plug Twilio or other provider
        _logger.LogInformation("Mock SMS → To: {Phone}, Message: {Message}",
            toPhoneNumber, message);

        return Task.CompletedTask;
    }
}

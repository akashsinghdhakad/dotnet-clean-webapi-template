using dotnetWebApiCoreCBA.Common;
using dotnetWebApiCoreCBA.Services.Implementations;
using dotnetWebApiCoreCBA.Services.Interfaces;

namespace dotnetWebApiCoreCBA.Configuration;

public static class ServiceConfig
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IAuthService, AuthService>();

        // Bind settings
        services.Configure<EmailSettings>(configuration.GetSection("Email"));
        services.Configure<SmsSettings>(configuration.GetSection("Sms"));

        // Register email & SMS services
        // services.AddScoped<IEmailService, EmailService>();
        // services.AddScoped<ISmsService, SmsService>();

        // Infrastructure helpers (side effects)
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ISmsService, SmsService>();

        // any other domain services in future

        return services;
    }
}

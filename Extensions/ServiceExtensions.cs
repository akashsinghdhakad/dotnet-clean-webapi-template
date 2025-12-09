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

        // any other domain services in future
        // services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}

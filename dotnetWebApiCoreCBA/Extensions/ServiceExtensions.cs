using dotnetWebApiCoreCBA.Common;
using dotnetWebApiCoreCBA.Services.Implementations;
using dotnetWebApiCoreCBA.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace dotnetWebApiCoreCBA.Configuration;

public static class ServiceConfig
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // FluentValidation via AutoValidation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Program>();

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

        services.AddExceptionHandler<dotnetWebApiCoreCBA.Middleware.GlobalExceptionHandler>();
        services.AddProblemDetails();

        // Health Checks
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!, name: "database", tags: new[] { "ready" });

        // Rate Limiting (Fixed Window Limit)
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1)
                    }));
            options.RejectionStatusCode = 429;
        });

        // Strict CORS Policy
        services.AddCors(options =>
        {
            options.AddPolicy("PremiumPolicy", builder =>
                builder.WithOrigins("http://localhost:3000", "https://yourproductiondomain.com")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
        });

        return services;
    }
}

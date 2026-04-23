using dotnetWebApiCoreCBA.Middleware;

namespace dotnetWebApiCoreCBA.Configuration;

public static class PipelineConfig
{
    public static WebApplication UseApplicationPipeline(this WebApplication app)
    {
        // Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("PremiumPolicy");

        // Custom middleware
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseExceptionHandler(); // .NET 8 Global Exception Handler

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRateLimiter();

        app.MapControllers();
        app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready")
        });
        app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            Predicate = _ => false // simple liveness check
        });

        // Optional root endpoint
        app.MapGet("/", () => "✅ dotnetWebApiCoreCBA API is running");

        return app;
    }
}

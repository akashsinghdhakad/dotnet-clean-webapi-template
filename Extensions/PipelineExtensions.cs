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

        // Custom middleware
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Optional root endpoint
        app.MapGet("/", () => "✅ dotnetWebApiCoreCBA API is running");

        return app;
    }
}

using DotnetWebApiCoreCBA.Data;
using DotnetWebApiCoreCBA.Middleware;
using DotnetWebApiCoreCBA.Repositories.Implementations.EfCore;
using DotnetWebApiCoreCBA.Repositories.Implementations.InMemory;
using DotnetWebApiCoreCBA.Repositories.Interfaces;
using DotnetWebApiCoreCBA.Services.Implementations;
using DotnetWebApiCoreCBA.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 1. Add services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Auth (wire up from config)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"]; // or your own
        options.Audience = builder.Configuration["Jwt:Audience"];
        // options.RequireHttpsMetadata = false; // for dev
    });

builder.Services.AddAuthorization();

// Register custom services
builder.Services.AddScoped<ITodoService, TodoService>();

// Choose repository implementation here:
// WITHOUT EF:
builder.Services.AddScoped<ITodoRepository, TodoRepositoryInMemory>();

// WITH EF (uncomment when using EF):
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddScoped<ITodoRepository, TodoRepositoryEf>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITodoRepository, TodoRepositoryEf>();


// Custom middleware dependencies if needed…

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Interceptor-like middleware
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Root default route
app.MapGet("/", () => "✅ Web API Template is running");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

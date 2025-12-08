using System.Text;
using DotnetWebApiCoreCBA.Common;
using DotnetWebApiCoreCBA.Data;
using DotnetWebApiCoreCBA.Middleware;
using DotnetWebApiCoreCBA.Repositories.Implementations.EfCore;
using DotnetWebApiCoreCBA.Repositories.Implementations.InMemory;
using DotnetWebApiCoreCBA.Repositories.Implementations.Sql;
using DotnetWebApiCoreCBA.Repositories.Interfaces;
using DotnetWebApiCoreCBA.Services.Implementations;
using DotnetWebApiCoreCBA.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 1. Add services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== JWT CONFIG =====
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSection);

var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // set true in production with HTTPS
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Register custom services
builder.Services.AddScoped<ITodoService, TodoService>();

// Choose repository implementation here:
// WITHOUT EF:
// builder.Services.AddScoped<ITodoRepository, TodoRepositoryInMemory>();

// WITH EF (uncomment when using EF):
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddScoped<ITodoRepository, TodoRepositoryEf>();

// WITH SQL:
// builder.Services.AddScoped<ITodoRepository, TodoRepositorySql>();

var repoMode = builder.Configuration["RepositoryMode"]; // "InMemory" / "Sql" / "Ef"

switch (repoMode)
{
    case "InMemory":
        builder.Services.AddScoped<ITodoRepository, TodoRepositoryInMemory>();
        break;

    case "Sql":
        builder.Services.AddScoped<ITodoRepository, TodoRepositorySql>();
        break;

    case "Ef":
    default:
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ITodoRepository, TodoRepositoryEf>();
        break;
}

builder.Services.AddScoped<IAuthService, AuthService>(); // add this




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

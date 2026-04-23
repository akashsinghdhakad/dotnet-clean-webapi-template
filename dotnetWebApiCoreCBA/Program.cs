using dotnetWebApiCoreCBA.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// Add services
builder.Services.AddControllers();

// OpenAPI / Swagger + JWT
builder.Services.AddApiDocumentation();

// Auth + JWT Bearer
builder.Services.AddJwtAuthentication(builder.Configuration);

// EF Core + DbContext + repositories
builder.Services.AddPersistence(builder.Configuration);

// API Versioning
builder.Services.AddApiVersioningConfig();

// Application-level services (TodoService, AuthService, etc.)
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// HTTP request pipeline: middleware + routing
app.UseSerilogRequestLogging();
app.UseApplicationPipeline();

app.Run();
using dotnetWebApiCoreCBA.Configuration;

var builder = WebApplication.CreateBuilder(args);

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

// Application-level services (TodoService, AuthService, etc.)
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// HTTP request pipeline: middleware + routing
app.UseApplicationPipeline();

app.Run();
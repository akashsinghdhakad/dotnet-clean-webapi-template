# 🚀 dotnetWebApiCoreCBA – Complete Web API Template (In-Memory + SQL + EF Core + JWT)

A production-ready, fully extensible **ASP.NET Core Web API Template** designed for real applications, demos, and rapid prototyping.

This template provides **three selectable data modes**, **JWT authentication**, **clean architecture**, **global middleware**, and **a professional folder structure**.

---

A clean, modern, and reusable **ASP.NET Core Web API template** designed for production-ready REST APIs.  
This template includes:

- Authentication-ready architecture  
- Global exception handling  
- Interceptor-style middleware  
- Layered (Service + Repository) structure  
- **EF Core** and **In-Memory** and **SQL (Raw ADO.NET)** data provider support  
- Clean folder structure  
- Standard API response format  
- Swagger/OpenAPI enabled automatically  

---

## 🔥 Features

### ✅ Clean & Scalable Architecture
- Controller → Service → Repository → Data layers  
- DTO models for request/response separation  
- Domain Entities isolated from API contracts  

### ✅ Built-in Middleware
- **Global Exception Handler**  
- **Request Logging Middleware** (acts like interceptors)  

### ✅ Data Access Options
Choose based on your project:

#### 1. **Without EF Core (In-Memory repository)**
- Lightweight  
- Great for testing  
- High performance  

#### 2. **SQL (Raw ADO.NET)**
- Direct SQL queries  
- No EF Core dependency  
- AppDbContext included  

#### 3. **With EF Core**
- SQL Server-ready  
- Easily switchable by DI  
- AppDbContext included  


### ✅ Routing & Swagger
- Attribute-based routing  
- `/swagger` UI for testing  

---

# 🌟 Highlights

### 🔥 **3 Repository Modes**
| Mode | Description |
|------|-------------|
| **In-Memory** | Fastest mode. No database required. Best for demos & testing. |
| **SQL (Raw ADO.NET)** | Direct SQL queries. No EF Core dependency. High performance. |
| **EF Core** | Full ORM support. Best for production-grade systems. |

Switch with **one line** in `Program.cs`.

---

# 🔐 JWT Authentication Ready

### ✅ Authentication Ready
- JWT Authentication plug-in  
- Authorization attributes ready  
- Controllers easily secured  

Features included:

- `/api/auth/login` endpoint  
- JWT token generator  
- Configurable secret keys  
- `[Authorize]` and `[AllowAnonymous]` support  

---

# 🧱 Architecture Overview

- **Controllers** → handle HTTP  
- **Services** → business logic  
- **Repositories** → data access  
- **Data Layer** → EF or SQL  
- **Middleware** → logging, exception handling  
- **DTOs** → clean request/response models  

---

# 📂 Folder Structure

```
dotnetWebApiCoreCBA/
│
├── Program.cs                  # Entry point – just wires everything together
├── appsettings.json            # Connection strings, Jwt settings, etc.
├── appsettings.Development.json
│
├── Common/
│   ├── ApiResponse.cs          # Standard response wrapper {success, data, message, errorCode}
│   └── JwtSettings.cs          # Strongly-typed config for Jwt (Key, Issuer, Audience, Expiry)
│
├── Data/
│   ├── AppDbContext.cs         # EF Core DbContext (Users, Todos, etc.)
│   └── AppDbContextFactory.cs  # Design-time factory for migrations (used by dotnet ef)
│
├── Models/
│   ├── Entities/
│   │   ├── User.cs             # User entity mapped to Users table
│   │   └── Todo.cs             # Todo entity mapped to Todos table
│   │
│   └── DTOs/
│       ├── Auth/
│       │   ├── RegisterRequest.cs
│       │   ├── LoginRequest.cs
│       │   └── LoginResponse.cs
│       │
│       └── Todo/
│           ├── TodoCreateRequest.cs
│           ├── TodoUpdateRequest.cs
│           └── TodoResponse.cs
│
├── Repositories/
│   ├── Interfaces/
│   │   ├── ITodoRepository.cs  # Abstraction for todo data access
│   │   └── IUserRepository.cs  # Abstraction for user data access
│   │
│   └── Implementations/
│       ├── EfCore/
│       │   ├── TodoRepositoryEf.cs   # Todo repo using EF Core
│       │   └── UserRepositoryEf.cs   # User repo using EF Core
│       │
│       ├── Sql/
│       │   └── TodoRepositorySql.cs  # Todo repo using raw SQL / ADO.NET
│       │
│       └── InMemory/
│           └── TodoRepositoryInMemory.cs  # Todo repo for testing / demo
│
├── Services/
│   ├── Interfaces/
│   │   ├── ITodoService.cs     # Business logic contract for todos
│   │   └── IAuthService.cs     # Business logic contract for auth
│   │
│   └── Implementations/
│       ├── TodoService.cs      # Todo business logic (uses ITodoRepository)
│       └── AuthService.cs      # Auth logic: register, login, hashing, JWT
│
├── Controllers/
│   ├── AuthController.cs       # /api/auth/register, /api/auth/login
│   └── TodoController.cs       # /api/todo CRUD (secured via [Authorize])
│
├── Middleware/
│   ├── RequestLoggingMiddleware.cs    # Logs incoming requests (path, method, etc.)
│   └── ExceptionHandlingMiddleware.cs # Global try/catch → returns unified error response
│
├── Extensions/                 # (previously Configuration/)
│   ├── JwtExtensions.cs        # AddJwtAuthentication(this IServiceCollection,…)
│   ├── SwaggerExtensions.cs    # AddApiDocumentation(this IServiceCollection)
│   ├── PersistenceExtensions.cs# AddPersistence(this IServiceCollection,… DbContext + repos)
│   ├── ServiceExtensions.cs    # AddApplicationServices(this IServiceCollection)
│   └── PipelineExtensions.cs   # UseApplicationPipeline(this WebApplication)
│
└── README.md                   # Docs for how to use this template

```

---

# ⚙️ Setup Instructions

## 1️⃣ Restore dependencies

```bash
dotnet restore
```

## 2️⃣ Run the API

```bash
dotnet run
```

Swagger available at:

```
/swagger
```

---

# 🔀 Choosing Repository Mode

Configured via `appsettings.json`:

```json
"RepositoryMode": "Ef" //InMemory, Ef, Sql
```
---

## ▶️ **Mode 1: In-Memory Repository** (No DB)

`Program.cs`:

```csharp
builder.Services.AddScoped<ITodoRepository, TodoRepositoryInMemory>();
```

Zero configuration required.

---

## ▶️ **Mode 2: SQL Repository (Raw ADO.NET)**

### Configure connection string

`appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TodoDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### Enable SQL mode

`Program.cs`:

```csharp
builder.Services.AddScoped<ITodoRepository, TodoRepositorySql>();
```

### SQL Table

```sql
CREATE TABLE Users (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Username] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
);
```
```sql
CREATE TABLE Todos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT(0)
);
```

---

## ▶️ **Mode 3: EF Core Repository**

`Program.cs`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITodoRepository, TodoRepositoryEf>();
```
`EF Core Commands`:

```bash
dotnet clean
dotnet build
dotnet ef migrations add InitialAuth
dotnet ef database update
dotnet ef database update 0   # rollback (dev only)
```
---

# 🔐 JWT Authentication

### appsettings.json:

```json
"Jwt": {
  "Key": "CHANGE_THIS_SECRET",
  "Issuer": "dotnetWebApiCoreCBA",
  "Audience": "dotnetWebApiCoreCBAClient",
  "ExpiresInMinutes": 60
}
```

### Login Endpoint

**POST** `/api/auth/login`

Request:

```json
{
  "username": "admin",
  "password": "admin123"
}
```

Response:

```json
{
  "success": true,
  "data": {
    "token": "<jwt_token>",
    "expiresAt": "2025-01-01T00:00:00Z",
    "username": "admin"
  }
}
```

---

# 🧪 API Response Structure

### ✔ Success Example

```json
{
  "success": true,
  "message": "Operation successful",
  "data": { }
}
```

### ❌ Error Example

```json
{
  "success": false,
  "errorCode": "VALIDATION_ERROR",
  "message": "Invalid request data"
}
```

---

## ⚙️ Dependency Injection

| Component | Lifetime |
|--------|--------|
| Controllers | Scoped |
| Services | Scoped |
| Repositories | Scoped |
| DbContext | Scoped |
| ConnectionFactory | Singleton |
| Email/SMS | Transient |

---
## 🧠 Rule of thumb (remember this)

- Business logic → Scoped
- Database access → Scoped
- Factories → Singleton
- External helpers (Email/SMS) → Transient

---

## 🧠 Architecture Name

- Clean Architecture
- Layered Architecture
- Repository Pattern
- Dependency Injection
- Middleware Pipeline

---

## ✅ Design Principles

- SOLID principles
- Separation of concerns
- Graceful failure
- Secure authentication
- Enterprise‑ready

---

## 🚀 Future Enhancements

- Refresh tokens
- Role‑based authorization
- Background jobs
- Message queues
- Unit & integration tests

---

## 👨‍💻 Notes

This template is suitable for:
- Learning projects
- Production APIs
- Interviews
- Government & enterprise systems

---

# 🛠 Tools & Technologies

- .NET 8 / .NET 9 / .NET 10 SDK  
- SQL Server (optional)  
- Entity Framework Core  
- Visual Studio Code  
- Swagger / Swashbuckle  

---

# 🤝 Contributing

1. Fork  
2. Create feature branch  
3. Commit changes  
4. Submit PR  

---

# 📄 License

Free to use, modify, and distribute.

---

# 🙋 Need More Features?

I can generate:

- Clean Architecture version  
- Multi-project enterprise scaffold  
- Dapper Repository Mode  
- MongoDB Mode  
- Auto repository selection via config  

Just ask anytime 🚀

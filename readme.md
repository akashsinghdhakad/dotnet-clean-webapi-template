# 🚀 dotnetWebApiCoreCBA – Web API Template

A clean, modern, and reusable **ASP.NET Core Web API template** designed for production-ready REST APIs.  
This template includes:

- Authentication-ready architecture  
- Global exception handling  
- Interceptor-style middleware  
- Layered (Service + Repository) structure  
- Both **EF Core** and **In-Memory** data provider support  
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
- No database required  

#### 2. **With EF Core**
- SQL Server-ready  
- Easily switchable by DI  
- AppDbContext included  

### ✅ Authentication Ready
- JWT Authentication plug-in  
- Authorization attributes ready  
- Controllers easily secured  

### ✅ Standard API Response Wrapper
All responses follow:

```json
{
  "success": true,
  "message": "OK",
  "data": { }
}
```

Or in case of errors:

```json
{
  "success": false,
  "errorCode": "INTERNAL_ERROR",
  "message": "Something went wrong"
}
```

### ✅ Routing & Swagger
- Attribute-based routing  
- `/swagger` UI for testing  

---

## 📂 Folder Structure

```
dotnetWebApiCoreCBA/
│
├── Controllers/
│     └── TodoController.cs
│
├── Models/
│     ├── Entities/
│     │     └── Todo.cs
│     └── DTOs/
│           └── Todo/
│                 ├── TodoCreateRequest.cs
│                 └── TodoResponse.cs
│
├── Services/
│     ├── Interfaces/
│     └── Implementations/
│           └── TodoService.cs
│
├── Repositories/
│     ├── Interfaces/
│     ├── Implementations/
│     │     ├── InMemory/
│     │     └── EfCore/
│
├── Middleware/
│     ├── ExceptionHandlingMiddleware.cs
│     └── RequestLoggingMiddleware.cs
│
├── Data/
│     └── AppDbContext.cs
│
├── Common/
│     └── ApiResponse.cs
│
├── Program.cs
└── README.md
```

---

## ⚙️ Setup Instructions

### 1️⃣ Install Dependencies

```bash
dotnet restore
```

### 2️⃣ Run the API

```bash
dotnet run
```

API will start at:

```
http://localhost:5000
https://localhost:7000
```

Swagger UI:

```
/swagger
```

---

## 🔀 Switching Between In-Memory & EF Core

### Use **In-Memory Repository** (default)

In `Program.cs`:

```csharp
builder.Services.AddScoped<ITodoRepository, TodoRepositoryInMemory>();
```

### Use **EF Core Repository**

Uncomment:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITodoRepository, TodoRepositoryEf>();
```

---

## 🔐 Authentication (Optional)

To enable JWT:

1. Add auth config in `Program.cs`  
2. Add authorization attributes:

```csharp
[Authorize]
public class TodoController : ControllerBase
```

3. Allow public routes:

```csharp
[AllowAnonymous]
```

---

## 🧪 API Conventions

### Success Example

```json
{
  "success": true,
  "message": "Todo created successfully",
  "data": {
    "id": 1,
    "title": "Learn .NET Core",
    "isCompleted": false
  }
}
```

### Error Example

```json
{
  "success": false,
  "errorCode": "NOT_FOUND",
  "message": "Todo not found"
}
```

---

## 🛠 Development Tools Used

- .NET 8/9/10 SDK  
- Swashbuckle (Swagger)  
- EF Core (Optional)  
- Visual Studio Code  

---

## 🤝 Contributing

1. Fork the repository  
2. Create a new feature branch  
3. Commit your changes  
4. Make a pull request  

---

## 📄 License

This template is free to modify and use in any project.

---

## 🙋 Need Help?

If you want enhancements like:
- Clean Architecture (multi-project) version  
- Auto code generator script  
- JWT login implementation  
- CI/CD ready template  

Just ask — I can generate these too.
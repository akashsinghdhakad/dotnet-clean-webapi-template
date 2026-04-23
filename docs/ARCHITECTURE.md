# 🧱 Clean Web API Architecture

This documentation outlines the structural foundation of the `.NET Core Web API Premium Template`. 
The application strictly enforces **Clean/Layered Architecture** principles, prioritizing Separation of Concerns (SoC) and SOLID design concepts.

---

## 🏗️ Structural Layers

The hierarchy consists of loosely-coupled components to enforce the Dependency Inversion Principle. No inner layer directly references the outer layer.

### 🌐 1. Presentation Layer (Controllers)
- **Role**: Entry point for external interfaces, managing HTTP Requests/Responses & HTTP status code mapping.
- **Rules**: Must contain exactly ZERO business logic.
- **Technologies**: 
  - `Asp.Versioning.Mvc` for structural route mapping (e.g., `v1`).
  - `FluentValidation` interceptors ensuring payloads meet conditions before mapping models.

### 🧠 2. Business Layer (Services)
- **Role**: Formulates application business rules, domain operations, calculations, and coordination. Models DTOs to Entity mappings.
- **Rules**: Cannot depend on API/HTTP concepts (like `HttpContext`). Depends only on Repository abstractions inside the DI container.
- **Components**: `ITodoService`, `IAuthService`.

### 💾 3. Data Access Layer (Repositories)
- **Role**: Provides data store mechanics (CRUD ops) isolated behind explicit interfaces (e.g., `ITodoRepository`).
- **Modes**:
  1. `In-Memory`: Used for ultra-fast UI testing and demos.
  2. `Raw ADO.NET`: Optimized flat-mapping queries using static SQL Commands.
  3. `Entity Framework Core`: Heavy ORM operations modeling the Entity DB Context dynamically.

### 🧩 4. Cross-Cutting Concerns / Common
- **Logging**: Configured Globally through `Serilog`.
- **Exception Handling**: Captured centrally avoiding leaky `try/catches` across the system. Handled through `IExceptionHandler` returning `ProblemDetails`.
- **Models**: Disconnected pure POCOs for Entities and specific DTOs separated between Request/Response schemas avoiding exposing domain traits.

---

## ⚡ Data Flow Pipeline (Execution Sequence)

1. **HTTP Client** sends JSON payload to `https://API_HOST/api/v1/auth/login`.
2. **Microsoft Rate Limiter** validates client usage frequency (`FixedWindowLimiter`).
3. **CORS Pipeline** authenticates Domain Origin properties.
4. **Middleware (Serilog Request Logging)** snaps timing metrics context.
5. **FluentValidation** inspects Request DTO structure, instantly throwing `400 Bad Request` if invalid.
6. **Controller** hands validated DTO payload to isolated `AuthService`.
7. `AuthService` engages `IUserRepository` via constructor injection logic.
8. `Repository` talks to Database and maps the DB Entity sequence safely back to the Service.
9. `Service` strips sensitive Entity data shaping a `LoginResponse` DTO structure.
10. `Controller` wraps standard `ApiResponse<T>` with HTTP Code `200 Success` outputting structural JSON payload.

---

## 🔒 Security Practices Configured
- Password Hash logic mapping dynamically using modern PBKDF2/BCrypt hashing patterns.
- JWT Lifespan rules preventing persistent stale key abuse.
- Security Policies (HSTS mappings conceptually active dynamically on Cloud environments).

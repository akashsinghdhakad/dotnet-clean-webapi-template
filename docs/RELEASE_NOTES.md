# 💿 Release Notes

## Version v1.0.0-premium (Latest)

The Web API has evolved from a foundational codebase into a fully structured, enterprise-ready premium web API template. 

### 🚀 New Features & Enhancements
- **.NET 8 Exception Interceptors**: Replaced the previous `ExceptionHandlingMiddleware` pattern with the newer `.NET 8` abstraction `IExceptionHandler` ensuring RFC mapped `ProblemDetails` exception propagation.
- **Data Protection & Rate Limiting**: Plugged in the `FixedWindowLimiter` pipeline intercepting any aggressive caller activity explicitly limiting connections globally to 100 requests per minute.
- **Observable Auditing (Serilog)**: Upgraded raw internal logging to explicit, structured `Serilog` mechanics configured robustly natively parsing configurations to daily synced `/logs/log.txt` outputs.
- **API Version Flow**: Deployed route versioning `[ApiVersion("1.0")]`. All default interactions are officially structured at `/api/v1/[controller]`.
- **Active Structural Diagnosing**: Deployed `AddHealthChecks()` evaluating background DB operations automatically reporting readiness capabilities directly to infrastructure load balancers.
- **Fluent Data Assurance**: Implemented `FluentValidation` intercepting model structures and strictly terminating requests mapping poorly configured JSON parameters instantaneously.
- **xUnit Validation Core**: Expanded the solution ecosystem generating `dotnetWebApiCoreCBA.Tests` natively bootstrapping `Moq` interfaces to test controller logics rapidly without external system dependencies.

---

## Version v1.0.0-base (Prior Engine)

- Successfully mapped independent Repository behaviors (`In-Memory`, `ADO.NET`, `EF Core`).
- Successfully bootstrapped standard Swagger API documentation templates.
- Registered fundamental JWT verification flows checking symmetric cryptographic hashes strictly across Controllers safely isolating `/public` vs `[Authorize]` interactions.

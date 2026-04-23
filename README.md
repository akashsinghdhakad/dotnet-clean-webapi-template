# 🚀 .NET Core Premium Web API Template

An extreme-grade, production-ready **ASP.NET Core Web API Template** expertly tailored for enterprise systems, dynamic architectures, scaling infrastructure, and rapid secure prototyping!

This is not a minimal template. This system possesses comprehensive infrastructure requirements for modern API applications configured directly out-of-the-box.

---

## 🔥 Enterprise Features Added (Premium Edition)

1. **Structured Auditable Logging**: Real-time analytical logs natively driven by `Serilog` (Console & Daily Rolling Files) to effortlessly monitor app states.
2. **Defensive API Resilience**: Completely avoids DOS manipulation by executing built-in `.NET FixedWindowRateLimiter` policies globally locking rogue clients. 
3. **Guarded Type Validations**: Auto-resolves DTO payload data configurations intercepting requests implicitly via `FluentValidation`.
4. **RFC Compliant Exception Handling**: Integrated standard .NET 8 `IExceptionHandler` pipeline pushing standard structural exception shapes dynamically to UI endpoints.
5. **Modern API Versioning Models**: Protects existing enterprise integrations using structured `/api/v{version}` paths out of the box dynamically via `Asp.Versioning.Mvc`.
6. **Infrastructure Health Mapping**: Generates Live `/health/live` and Database verification `/health/ready` endpoints explicitly tested directly natively natively.
7. **TDD / Testing Pipeline Ready**: Built around decoupled injected patterns easily allowing structural integrations validated through parallel `xUnit` mechanisms checking isolated logic rules confidently.

## 🧱 Repository Options Available Contextually

Swap persistence techniques by changing exactly **one injection line** logically!

| Repository Mode | Recommended Application Model | Benefits |
|------|-------------|-----------|
| **In-Memory** (`TodoRepositoryInMemory`) | UI Wireframing / Technical Prototyping | Instant startup speeds without infrastructure complexity mapping explicitly mapping properties implicitly. |
| **Raw SQL** (`TodoRepositorySql`) | High-Speed Microservices / Scalability Tasks | Fast, explicit data fetching modeling bypassing extensive ORM overheads automatically using pure queries. |
| **EF Core** (`TodoRepositoryEf`) | Complete Dynamic Enterprise Production Models | Auto-migration deployment handling mappings safely without context execution logic. |

---

## ⚙️ Getting Started Quickly

### 1️⃣ Initialization Setup
Clone the structure context ensuring references are parsed automatically:
```bash
dotnet restore
```

### 2️⃣ Run Development Tests Context
Initialize API validation environments easily to inspect logic implicitly running.
```bash
dotnet test
```

### 3️⃣ Launch System Safely!
```bash
dotnet run
```
Swagger UI will be loaded actively parsing API interactions directly via:
**`https://localhost:<port>/swagger/index.html`**

---

## 📂 Important Project Links
- [System Architecture (SoC Model)](./docs/ARCHITECTURE.md)
- [Application API Interaction Flow Model](./docs/FLOW.md)
- [System Version Iteration Changes](./docs/RELEASE_NOTES.md)

---

## 🔐 Secure Usage Examples (Authentication Structure)

Generate active testing tokens hitting dynamic controllers easily mapping explicitly via specific HTTP rules:

**POST Payload (`/api/v1/auth/login`)**:
```json
{
  "username": "admin",
  "password": "securepassword123"
}
```

Produces standard structural standard data shapes structurally mapping securely explicitly.

```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "token": "eyJhb...",
    "expiresAt": "2026-05-01T12:00:00Z",
    "username": "admin"
  }
}
```

---

## 🛠 Tech Stack Footprint

- Microsoft Server SDK: **.NET 8+**
- Structural Design: **Clean Layered Architecture**
- Testing Models: **xUnit & Moq**
- Observational Logistics: **Serilog & Diagnostics.HealthChecks**
- Validation Tooling: **FluentValidation** 
- Deployment Tooling: **Docker integration compatible structurally (Planned)**

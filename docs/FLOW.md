# 🔄 System Interaction Flows

This document visualizes how data requests traverse the API to clarify expected behaviors for external integration, troubleshooting, and debugging.

---

## 🔑 Authentication & Login Flow

```mermaid
sequenceDiagram
    participant C as Client (Browser/Mobile)
    participant API
    participant DB as Database
    
    C->>API: POST /api/v1/auth/login {username, password}
    Note over API: 1. Request passes Global Rate Limiter
    Note over API: 2. Request payload verified by FluentValidation
    API->>DB: Query User by matching plain-text username
    DB-->>API: Returns User Entity (including PBKDF2 hash components)
    Note over API: 3. Verify computed hash matches Entity PasswordHash
    API->>API: 4. Generate JWT with Claims {name, unique_name, role} & Expiry Target
    API-->>C: 200 OK - Return { token, expiresAt, username }
```

## ✅ CRUD Execution Flow (Standard Operations)

```mermaid
sequenceDiagram
    participant C as Client
    participant API as TodoController
    participant S as TodoService
    participant R as TodoRepository
    participant DB as SQL Database
    
    C->>API: GET /api/v1/todos/1 (with Auth Header Bearer Token)
    Note over API: Middleware evaluates & authorizes JWT Key
    API->>S: Calls GetByIdAsync(1)
    S->>R: Calls GetByIdAsync(1)
    R->>DB: Executes `SELECT * FROM Todos WHERE Id = 1`
    DB-->>R: Returns Entity SQL Data Model
    R-->>S: Entity Model
    Note over S: Maps Entity -> DTO mapping
    S-->>API: TodoResponse DTO
    Note over API: Wraps structure into standardized ApiResponse<TodoResponse>
    API-->>C: 200 OK JSON Output
```

## 💥 Global Exception Pipeline Mechanism Flow

```mermaid
sequenceDiagram
    participant C as Client
    participant P as Pipeline Ext
    participant Controller
    participant E as IExceptionHandler
    
    C->>Controller: Malformed or Faulty Request / Internal App Bug occurs
    Controller--xController: NullReferenceException thrown independently!
    Note over Controller,P: Operation instantly ceases
    P->>E: Exception natively intercepted by GlobalExceptionHandler logic.
    Note over E: Logs severity error safely down to Serilog Sinks globally tracking Exception state
    E->>E: Transforms Raw Code Exception into HTTP ProblemDetails schema
    E-->>C: 500 INTERNAL SERVER ERROR (Clean non-leaky JSON)
```

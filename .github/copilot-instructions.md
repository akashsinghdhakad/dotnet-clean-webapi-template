# Copilot Instructions for dotnetWebApiCoreCBA

## Project Overview
This project is a .NET Core Web API designed to manage a Todo application. It follows a layered architecture, separating concerns into Controllers, Services, Repositories, and Models. The main components include:

- **Controllers**: Handle HTTP requests and responses. Example: `TodoController.cs` manages Todo-related endpoints.
- **Services**: Contain business logic. Example: `TodoService.cs` provides methods for managing Todos.
- **Repositories**: Interact with the data source. Example: `TodoRepositoryEf.cs` uses Entity Framework Core for database operations.
- **Models**: Define the data structures. Example: `Todo.cs` represents the Todo entity.

## Developer Workflows
- **Building the Project**: Use the command `dotnet build` to compile the project.
- **Running the Project**: Start the application with `dotnet run`.
- **Testing**: Execute tests using `dotnet test`. Ensure all tests are passing before committing changes.

## Project Conventions
- **Naming Conventions**: Use PascalCase for class names and camelCase for method parameters.
- **Error Handling**: Centralized error handling is implemented in `ExceptionHandlingMiddleware.cs`.

## Integration Points
- **Database**: The application uses Entity Framework Core for data access. Configuration is found in `AppDbContext.cs`.
- **External Dependencies**: Ensure to install necessary NuGet packages for Entity Framework and any other libraries used.

## Communication Patterns
- **Service to Repository**: Services call repositories to perform data operations. Example: `TodoService` calls `ITodoRepository` methods.
- **Controller to Service**: Controllers invoke service methods to handle requests. Example: `TodoController` uses `TodoService` to manage Todos.

## Additional Notes
- Review the `launchSettings.json` in the `Properties` folder for environment-specific configurations.
- Follow the structure of existing files to maintain consistency across the codebase.

---

These instructions are designed to help AI agents understand the architecture and workflows of the dotnetWebApiCoreCBA project effectively.
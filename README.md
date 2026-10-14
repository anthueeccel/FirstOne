![.Net](http://img.shields.io/badge/-Core%20v3.1-008999?style=plastic&logo=.net&logoColor=ffffff) ![last_commit](https://img.shields.io/github/last-commit/anthueeccel/FirstOne) ![license](https://img.shields.io/github/license/anthueeccel/FirstOne)

### Overview

This repository, **FirstOne**, is a backend boilerplate/reference project built with **.NET Core 3.1**. It demonstrates a modern, enterprise-grade architecture focusing on **CQRS (Command Query Responsibility Segregation)**, **Domain-Driven Design (DDD)** principles, and **decoupled messaging**. This project originated in 2020 as a study project to learn about architecture, authentication, and permission management.

Here is an overview of what the project does and the technologies it uses:

---

### 1. Core Purpose
The project is a system for managing **Registrations (Cadastros)**, specifically focusing on **Pessoas (Persons)** and **Usuários (Users)**. It provides a RESTful API to handle CRUD operations, authentication, and permission management (Claims).

### 2. Technology Stack
*   **Framework:** .NET Core 3.1
*   **Architectural Patterns:** 
    *   **CQRS:** Separates "commands" (writes/actions) from "queries" (reads).
    *   **Mediator Pattern:** Uses **MediatR** to decouple the controllers from the logic handlers.
    *   **Repository Pattern & Unit of Work:** For data abstraction and ensuring transaction integrity.
    *   **Domain Notifications:** A pattern to collect validation errors and return them to the UI without throwing exceptions.
*   **Database & ORM:** 
    *   **Entity Framework Core:** Primary ORM for SQL Server.
    *   **SQL Server:** The main relational database.
    *   **MongoDB:** Included in the infrastructure layer (though the core logic currently points to SQL Server).
*   **Security:** 
    *   **JWT (JSON Web Tokens):** For stateless authentication.
    *   **Custom Claims-Based Authorization:** A custom attribute (`ClaimsAuthorization`) to restrict access based on user permissions.
*   **Mapping & Validation:** 
    *   **AutoMapper:** Maps Domain Entities to ViewModels.
    *   **FluentValidation:** Used to validate Commands (e.g., ensuring a name is not empty before saving).
*   **Testing:** 
    *   **xUnit:** Testing framework.
    *   **Moq & Moq.AutoMock:** For mocking dependencies in unit tests.
    *   **FluentAssertions:** For readable test assertions.

---

### 3. Project Structure (Clean Architecture)
The solution is divided into layers to ensure a separation of concerns:

*   **API Layer (`FirstOne.Cadastros.Api`):**
    *   Contains Controllers (`PessoaController`, `UsuarioController`).
    *   Handles HTTP requests, JWT configuration, and dependency injection.
*   **Application Layer (`FirstOne.Cadastros.Application`):**
    *   Acts as the orchestrator.
    *   Contains **Services** that convert ViewModels into Commands and send them to the Mediator.
    *   Handles Token generation and Login logic.
*   **Domain Layer (`FirstOne.Cadastros.Domain`):**
    *   The "Heart" of the system.
    *   Contains **Entities** (Pessoa, Usuario), **Commands**, **Command Handlers**, and **Interfaces**.
    *   This is where the business rules live (e.g., `AddPessoaCommandValidation`).
*   **Infrastructure Layer (`FirstOne.Cadastros.Infra.Data`):**
    *   Handles data persistence.
    *   Contains the EF Core `SqlServerContext`, Migrations, and Repository implementations.
*   **Tests:**
    *   Extensive unit testing for the API, Application services, and Domain logic.

---

### 4. Key Workflow Example: Adding a Person
1.  **API:** `PessoaController` receives a POST request with a `PessoaViewModel`.
2.  **Application:** `PessoaAppService` receives the ViewModel, maps it to an `AddPessoaCommand`, and checks if it's valid.
3.  **Domain (Mediator):** The command is sent via MediatR to the `PessoaCommandHandler`.
4.  **Domain (Validation):** The handler/command uses `FluentValidation` to check business rules. If it fails, a `DomainNotification` is raised.
5.  **Infrastructure:** If valid, the `PessoaRepository` saves the data to the SQL Server database via the Unit of Work (Commit).
6.  **Response:** The Controller checks the `DomainNotificationHandler`. If errors exist, it returns a `422 Unprocessable Entity`; otherwise, it returns a `200 OK`.

### 5. Notable Features
*   **Custom Authorization:** It has a robust permission system. Instead of just "Roles," it checks specific "Claims" for actions like "Add," "Update," or "Remove" on specific entities.
*   **Seeded Data:** The project includes migrations that automatically populate the database with a "Tester" user and initial permissions upon setup.
*   **Error Handling:** It avoids `try-catch` blocks for validation by using the Notification Pattern, which is generally more performant and cleaner for business rule violations.

```
Directory structure:
└── anthueeccel-firstone/
    ├── README.md
    ├── FirstOne.sln
    ├── FirstOne.Cadastros.Application.Tests/
    │   ├── FirstOne.Cadastros.Application.Tests.csproj
    │   ├── PessoaAppServiceTests.cs
    │   └── UsuarioAppServiceTests.cs
    ├── FirstOne.Cadastros.Domain.Tests/
    │   ├── FirstOne.Cadastros.Domain.Tests.csproj
    │   ├── CommandHandlerTests/
    │   │   ├── PessoaCommandHandlerTests.cs
    │   │   └── UsuarioCommandHandlerTests.cs
    │   └── Entities/
    │       ├── PessoaTest.cs
    │       ├── UsuarioClaimTests.cs
    │       └── UsuarioTests.cs
    ├── src/
    │   ├── FirstOne.Cadastros.Api/
    │   │   ├── appsettings.Development.json
    │   │   ├── appsettings.json
    │   │   ├── CustomAuthorize.cs
    │   │   ├── FirstOne.Cadastros.Api.csproj
    │   │   ├── Program.cs
    │   │   ├── ServicesRegistrator.cs
    │   │   ├── Startup.cs
    │   │   ├── Controllers/
    │   │   │   ├── Controller.cs
    │   │   │   ├── HealthCheckController.cs
    │   │   │   ├── PessoaController.cs
    │   │   │   └── UsuarioController.cs
    │   │   └── Properties/
    │   │       └── launchSettings.json
    │   ├── FirstOne.Cadastros.Application/
    │   │   ├── FirstOne.Cadastros.Application.csproj
    │   │   ├── AutoMapper/
    │   │   │   └── DomainToViewModelMappingProfile.cs
    │   │   ├── Interfaces/
    │   │   │   ├── IPessoaAppService.cs
    │   │   │   └── IUsuarioAppService.cs
    │   │   ├── Services/
    │   │   │   ├── AppService.cs
    │   │   │   ├── PessoaAppService.cs
    │   │   │   └── UsuarioAppService.cs
    │   │   ├── Token/
    │   │   │   └── TokenSettings.cs
    │   │   └── ViewModels/
    │   │       ├── LoginViewModel.cs
    │   │       ├── PessoaViewModel.cs
    │   │       ├── UsuarioClaimViewModel.cs
    │   │       └── UsuarioViewModel.cs
    │   ├── FirstOne.Cadastros.Domain/
    │   │   ├── FirstOne.Cadastros.Domain.csproj
    │   │   ├── CommandHandler/
    │   │   │   ├── PessoaCommandHandler.cs
    │   │   │   └── UsuarioCommandHandler.cs
    │   │   ├── Commands/
    │   │   │   ├── AddPessoaCommand.cs
    │   │   │   ├── Command.cs
    │   │   │   ├── RemovePessoaCommand.cs
    │   │   │   ├── UpdatePessoaCommand.cs
    │   │   │   └── Usuario/
    │   │   │       └── AddUsuarioCommand.cs
    │   │   ├── Entities/
    │   │   │   ├── EntidadeBase.cs
    │   │   │   ├── Pessoa.cs
    │   │   │   ├── Usuario.cs
    │   │   │   └── UsuarioClaim.cs
    │   │   ├── Enums/
    │   │   │   └── EntidadeEnum.cs
    │   │   ├── Interfaces/
    │   │   │   ├── IPessoaRepository.cs
    │   │   │   ├── IRepository.cs
    │   │   │   ├── IUnitOfWork.cs
    │   │   │   └── IUsuarioRepository.cs
    │   │   ├── Mediator/
    │   │   │   ├── IMediatorHandler.cs
    │   │   │   └── MediatorHandler.cs
    │   │   ├── Messaging/
    │   │   │   ├── DomainNotification.cs
    │   │   │   ├── DomainNotificationHandler.cs
    │   │   │   └── ValidationMessages.cs
    │   │   └── Validations/
    │   │       ├── AddPessoaCommandValidation.cs
    │   │       ├── RemovePessoaCommandValidation.cs
    │   │       ├── UpdatePessoaCommandValidation.cs
    │   │       └── Usuario/
    │   │           └── AddUsuarioCommandValidation.cs
    │   └── FirstOne.Cadastros.Infra.Data/
    │       ├── FirstOne.Cadastros.Infra.Data.csproj
    │       ├── Context/
    │       │   └── SqlServerContext.cs
    │       ├── Migrations/
    │       │   ├── 20201026193044_Inicial.cs
    │       │   ├── 20201026193044_Inicial.Designer.cs
    │       │   ├── 20201027182256_PopulateData.cs
    │       │   ├── 20201027182256_PopulateData.Designer.cs
    │       │   ├── 20201030181817_AddedRoleToUsuario.cs
    │       │   ├── 20201030181817_AddedRoleToUsuario.Designer.cs
    │       │   └── SqlServerContextModelSnapshot.cs
    │       └── Repositories/
    │           ├── PessoaRepository.cs
    │           ├── Repository.cs
    │           └── UsuarioRepository.cs
    └── tests/
        └── FirstOne.Cadastros.Api.Tests/
            ├── FirstOne.Cadastros.Api.Tests.csproj
            └── ControllerTests/
                ├── PessoaControllerTests.cs
                └── UsuarioControllerTests.cs
```

### Summary
This is a **starter template** for a microservice or a web API that needs to be scalable and maintainable. It follows modern C# best practices and is well-structured for team environments where unit testing and architectural consistency are priorities.

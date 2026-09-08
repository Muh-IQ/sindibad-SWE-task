# sindibad-SWE-task

A RESTful API for managing Projects and Tasks, built with ASP.NET Core and Entity Framework Core.

# Implementation Scope & Time Constraints

Due to the limited implementation timeframe, the project focuses on the core assignment requirements and clean API design.

There was not enough time to implement pagination using **cursor-based pagination**, handle all possible edge cases, or implement the remaining optional features.

The implementation prioritizes:

- Correct Project–Task relationships.
- Proper request validation.
- Appropriate HTTP status codes.
- Meaningful error handling.
- A consistent API response structure across endpoints, making the API easier for clients to consume and maintain.
- Clear separation of responsibilities between API, Application, Domain, and Infrastructure layers.
- Entity Framework Core migrations and database initialization.
- A clean and maintainable project structure.



## Setup and Run

### Prerequisites

* .NET 8 SDK
* Microsoft SQL Server
* Visual Studio 2022 or another compatible IDE
* Git
* Entity Framework Core CLI (`dotnet-ef`)

If `dotnet-ef` is not installed, install it using:

```bash
dotnet tool install --global dotnet-ef
```

### Clone the Repository

```bash
git clone <repository-url>

cd ManageProjects
```

Restore the project dependencies:

```bash
dotnet restore
```

## Database Configuration

The application reads the SQL Server connection string from the following environment variable:

```text
SINDIBAD____DB_CONNECTION
```

Set the variable to:

```text
Server=.;Database=Sindibad;Integrated Security=SSPI;TrustServerCertificate=True;
```

On Windows, the environment variable can be added through:

```text
System Properties
→ Advanced
→ Environment Variables
```

After adding or modifying the environment variable, restart Visual Studio or the terminal.

## Database Initialization

The project uses Entity Framework Core migrations to create and update the database schema.

Because the solution contains separate Infrastructure and API projects, run:

```bash
dotnet ef database update \
  --project src/Sindibad.Infrastructure \
  --startup-project src/Sindibad.Api
```

Alternatively, from Visual Studio Package Manager Console:

```powershell
Update-Database -Project Sindibad.Infrastructure -StartupProject Sindibad.Api
```

The existing migrations will create the required database schema, including the relationship between Projects and Tasks.

## Run the Application

Run the application from Visual Studio, or use:

```bash
dotnet run --project src/Sindibad.Api
```

Once the application is running, Swagger UI is available at:

```text
https://localhost:<port>/swagger
```

The exact port may vary depending on the local launch configuration.

## API Endpoints

The API uses URL-based versioning through the `/api/v1` route prefix.

### Projects

| Method | Endpoint                | Description                              |
| ------ | ----------------------- | ---------------------------------------- |
| GET    | `/api/v1/projects`      | Retrieve all projects                    |
| POST   | `/api/v1/projects`      | Create a new project                     |
| GET    | `/api/v1/projects/{id}` | Retrieve a project with all of its tasks |

### Tasks

| Method | Endpoint                             | Description                 |
| ------ | ------------------------------------ | --------------------------- |
| POST   | `/api/v1/projects/{projectId}/tasks` | Create a task for a project |
| PUT    | `/api/v1/tasks/{id}`                 | Update an existing task     |
| DELETE | `/api/v1/tasks/{id}`                 | Delete an existing task     |

Requests can be tested using Swagger UI.

## API Behavior

### Validation

Required fields are validated using FluentValidation.

Invalid requests return:

```text
400 Bad Request
```

with a consistent API response containing the validation errors.

### Not Found

When a requested Project or Task does not exist, the API returns:

```text
404 Not Found
```

with a meaningful error message.

### Create Operations

A successfully created Project or Task returns:

```text
201 Created
```

### Update Operations

A successfully updated Task returns:

```text
200 OK
```

### Delete Operations

A successfully deleted Task returns:

```text
204 No Content
```

## Data Model

The application contains two main entities:

### Project

* `Id`
* `Name`
* `CreatedAt`

### Task

* `Id`
* `ProjectId`
* `Title`
* `Completed`
* `CreatedAt`

Each Task belongs to an existing Project through the `ProjectId` foreign key.

The relationship is:

```text
Project 1 ─────────── * Task
```

When a Project is deleted, all Tasks associated with that Project are also deleted through the configured cascade delete behavior.

## Assumptions

* Soft delete is not used. Deleted Tasks are permanently removed from the database.
* Project names are limited to a maximum of 50 characters.
* Task titles are limited to a maximum of 150 characters.
* Every Task must belong to an existing Project through `ProjectId`.
* A Task cannot be reassigned to a different Project through the update endpoint.
* `Id` values are generated as GUIDs.
* `CreatedAt` values are generated using UTC time.
* When a Project is deleted, its associated Tasks are also deleted.
* The API uses explicit URL-based versioning through `/api/v1`.


## Limitations

* Pagination for `GET /api/v1/projects` was not implemented.
* Task filtering by completion status was not implemented.
* No caching or additional performance optimization techniques were implemented because they were outside the scope of the assignment.
* Unit test coverage is limited. A unit test for the Create Project service was implemented, while additional unit and integration tests were not implemented within the available timeframe.

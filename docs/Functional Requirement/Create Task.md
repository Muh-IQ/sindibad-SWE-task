### Create Task — Workflow

**Endpoint**

`POST /api/v1/projects/{projectId}/tasks`

**Input**

The client provides the project ID as a route parameter and the task title in the request body:

```text
POST /api/v1/projects/3fa85f64-5717-4562-b3fc-2c963f66afa6/tasks
````

```json
{
  "title": "Implement payment processing"
}
```

### Workflow

1. Client sends `POST /api/v1/projects/{projectId}/tasks` with the project ID and task title.
2. `TasksController` receives the HTTP request and binds the route parameter to a `Guid`.
3. The configured FluentValidation pipeline validates the `CreateTaskRequest`.
4. If validation fails:

   * `CreateTaskRequestValidator` detects the validation errors.
   * The request is rejected before reaching the Application layer.
   * The validation errors are converted into the unified `ApiResponse`.
   * API returns `400 Bad Request`.
5. If validation succeeds, `TasksController` calls `ITaskService.CreateAsync(projectId, request.Title)`.
6. `TaskService` checks whether the specified project exists.
7. `TaskService` uses `IGenericRepository<Project>` to check whether the project exists.
8. `GenericRepository<Project>` queries the `Projects` table through `AppDbContext` to check for the existence of the project.
9. `GenericRepository<Project>` returns a boolean indicating whether the project exists without returning the project entity.
10. If the project does not exist, `TaskService` detects that the existence check returned `false`.
11. If the project was not found:

    * `TaskService` creates a failure `Result` using the Result Pattern.
    * The failure represents that the requested project does not exist.
    * `TaskService` returns the failure `Result` to `TasksController`.
12. `TasksController` converts the failed `Result` into the unified `ApiResponse`.
13. API returns `404 Not Found`.
14. If the project exists, `TaskService` creates a new `Task` entity.
15. `TaskService` generates a new `Guid` for the task ID.
16. `TaskService` assigns the provided `projectId` to the task.
17. `TaskService` assigns the provided title to the task.
18. `TaskService` sets `Completed` to `false`.
19. `TaskService` sets `CreatedAt` using `DateTime.UtcNow`.
20. `TaskService` uses `IGenericRepository<Task>` to persist the new task.
21. `GenericRepository<Task>` adds the task to `AppDbContext`.
22. Entity Framework Core persists the new task to the `Tasks` table in SQL Server.
23. `GenericRepository<Task>` returns the created `Task` entity.
24. `TaskService` creates a successful `Result<Task>` containing the created task.
25. `TaskService` returns the successful `Result<Task>` to `TasksController`.
26. `TasksController` converts the successful result into the unified `ApiResponse<Task>`.
27. API returns `201 Created`.

### Success Response

```json
{
  "success": true,
  "data": {
    "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
    "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Implement payment processing",
    "completed": false,
    "createdAt": "2026-09-08T11:00:00Z"
  }
}
```

HTTP status:

`201 Created`

### Project Not Found

If the specified project does not exist:

```json
{
  "success": false,
  "message": "Project not found.",
  "errors": [
    "Project not found."
  ]
}
```

HTTP status:

`404 Not Found`

### Validation Failure

If the task title is missing or invalid:

```json
{
  "success": false,
  "message": "Validation Error",
  "errors": [
    "Task title is required."
  ]
}
```

HTTP status:

`400 Bad Request`

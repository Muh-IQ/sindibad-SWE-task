### Create Project — Workflow

**Endpoint**

`POST /projects`

**Input**

The client sends a project name in the request body:

```json
{
  "name": "Payment Platform"
}
```

### Workflow

1. Client sends `POST /projects` with the project name.
2. `ProjectsController` receives the HTTP request.
3. **API layer validates the request using FluentValidation.**
4. If validation fails:

   * Stop the workflow.
   * Do not call the Application layer.
   * Convert the validation errors into the unified `ApiResponse`.
   * Return `400 Bad Request`.
5. If validation succeeds, `ProjectsController` passes the project name to `IProjectService`.
6. `ProjectService` creates a new `Project` domain entity.
7. Generate a new `Guid` for `Project.Id`.
8. Set `CreatedAt` using the current UTC time.
9. `ProjectService` passes the entity to `IGenericRepository<Project>`.
10. `GenericRepository<Project>` adds the entity to `AppDbContext`.
11. EF Core persists the project to the `Projects` table in SQL Server.
12. Repository returns the created `Project` entity.
13. `ProjectService` returns `Result<Project>` using the Result Pattern.
14. `ProjectsController` handles the returned `Result<Project>`.
15. If the result is successful, the Controller converts the `Project` into `ApiResponse<Project>`.
16. API returns `201 Created`.

### Success Response

```json
{
  "success": true,
  "data": {
    "id": "guid",
    "name": "Payment Platform",
    "createdAt": "2026-09-08T00:00:00Z"
  }
}
```

### Validation Failure

FluentValidation catches invalid input before the Application layer.

The validation errors are converted into the unified `ApiResponse`:

```json
{
  "success": false,
  "message": "Validation failed.",
  "errors": [
    "Project name is required."
  ]
}
```

HTTP status:

`400 Bad Request`

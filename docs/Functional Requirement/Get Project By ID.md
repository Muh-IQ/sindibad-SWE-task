### Get Project By ID — Workflow

**Endpoint**

`GET /api/v1/projects/{id}`

**Input**

The client provides the project ID as a route parameter:

```text
GET /api/v1/projects/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

### Workflow

1. Client sends `GET /api/v1/projects/{id}` with the project ID.

2. `ProjectsController` receives the HTTP request and binds the route parameter to a `Guid`.

3. `ProjectsController` calls `IProjectService.GetByIdAsync(id)`.

4. `ProjectService` calls `IProjectRepository.GetByIdAsync(id)`.

5. `ProjectRepository` queries the `Projects` table through `AppDbContext`.

6. `ProjectRepository` includes the related `Tasks` using Entity Framework Core.

7. EF Core retrieves the requested project together with all of its tasks from SQL Server.

8. If the project exists, `ProjectRepository` returns the `Project` entity with its populated `Tasks` collection.

9. If no project is found, `ProjectRepository` returns `null`.

10. `ProjectService` checks whether the returned project is `null`.

11. If the project was not found:

    * `ProjectService` creates a failure `Result` using the Result Pattern.
    * The failure represents that the requested project does not exist.
    * `ProjectService` returns the failure `Result` to `ProjectsController`.

12. `ProjectsController` converts the failed `Result` into the unified `ApiResponse`.

13. API returns `404 Not Found`.

14. If the project exists, `ProjectService` creates a successful `Result<Project>` containing the project and its tasks.

15. `ProjectService` returns the successful `Result<Project>` to `ProjectsController`.

16. `ProjectsController` converts the successful result into the unified `ApiResponse<Project>`.

17. API returns `200 OK`.

### Success Response

```json
{
  "success": true,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Payment Platform",
    "createdAt": "2026-09-08T10:30:00Z",
    "tasks": [
      {
        "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
        "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "title": "Implement payment processing",
        "completed": false,
        "createdAt": "2026-09-08T11:00:00Z"
      },
      {
        "id": "9b2f8f20-2c1e-4b7c-9a10-123456789abc",
        "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "title": "Add wallet support",
        "completed": true,
        "createdAt": "2026-09-08T12:00:00Z"
      }
    ]
  }
}
```

HTTP status:

`200 OK`

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

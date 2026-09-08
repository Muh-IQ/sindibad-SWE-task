### Get Projects — Workflow

**Endpoint**

`GET /api/v1/projects`

**Input**

The client does not send a request body.

### Workflow

1. Client sends `GET /api/v1/projects`.

2. `ProjectsController` receives the HTTP request.

3. `ProjectsController` calls `IProjectService.GetAllAsync()`.

4. `ProjectService` calls `IProjectRepository.GetAllAsync()`.

5. `ProjectRepository` queries the `Projects` table through `AppDbContext`.

6. EF Core retrieves the projects from SQL Server.

7. `ProjectRepository` converts each `Project` entity into a `ProjectDTO`.

8. `ProjectRepository` returns the collection of `ProjectDTO` objects to `ProjectService`.

9. `ProjectService` checks whether the returned collection contains any projects.

10. If no projects are found:

    * `ProjectService` creates a failure `Result` using the Result Pattern.
    * The failure represents that no projects were found.
    * `ProjectService` returns the failure `Result` to `ProjectsController`.

11. `ProjectsController` converts the failed `Result` into the unified `ApiResponse`.

12. API returns `404 Not Found`.

13. If projects are found, `ProjectService` creates a successful `Result<List<ProjectDTO>>` containing the projects.

14. `ProjectService` returns the successful `Result<List<ProjectDTO>>` to `ProjectsController`.

15. `ProjectsController` converts the successful result into the unified `ApiResponse<List<ProjectDTO>>`.

16. API returns `200 OK`.

### Success Response

```json
{
  "success": true,
  "data": [
    {
      "id": "11111111-1111-1111-1111-111111111111",
      "name": "Payment Platform",
      "createdAt": "2026-09-08T00:00:00Z"
    },
    {
      "id": "22222222-2222-2222-2222-222222222222",
      "name": "School Management System",
      "createdAt": "2026-09-08T01:00:00Z"
    }
  ]
}
```

HTTP status:

`200 OK`

### No Projects Found

If the repository returns an empty collection:

```json
{
  "success": false,
  "message": "No projects found.",
  "errors": [
    "No projects found."
  ]
}
```

HTTP status:

`404 Not Found`

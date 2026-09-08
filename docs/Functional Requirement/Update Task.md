**### Update Task — Workflow**

****Endpoint****

`PUT /api/v1/tasks/{id}`

****Input****

The client provides the task ID as a route parameter and the updated task data in the request body:

```text
PUT /api/v1/tasks/7c9e6679-7425-40de-944b-e07fc1f90ae7
```

```json
{
  "title": "Implement payment processing",
  "completed": true
}
```

**### Workflow**

1. Client sends `PUT /api/v1/tasks/{id}` with the task ID and updated task data.

2. `TasksController` receives the HTTP request and binds the route parameter to a `Guid`.

3. The configured FluentValidation pipeline validates the `UpdateTaskRequest`.

4. If validation fails:

* `UpdateTaskRequestValidator` detects the validation errors.

* The request is rejected before reaching the Application layer.

* The validation errors are converted into the unified `ApiResponse`.

* API returns `400 Bad Request`.

5. If validation succeeds, `TasksController` calls `ITaskService.UpdateAsync(id, request)`.

6. `TaskService` retrieves the specified task.

7. `TaskService` uses `IGenericRepository<Task>` to retrieve the task by its ID.

8. `GenericRepository<Task>` queries the `Tasks` table through `AppDbContext`.

9. `GenericRepository<Task>` returns the matching `Task` entity if it exists, otherwise returns `null`.

10. If the task does not exist, `TaskService` detects that the repository returned `null`.

11. If the task was not found:

```
\* `TaskService` creates a failure `Result` using the Result Pattern.

\* The failure represents that the requested task does not exist.

\* `TaskService` returns the failure `Result` to `TasksController`.
```

12. `TasksController` converts the failed `Result` into the unified `ApiResponse`.

13. API returns `404 Not Found`.

14. If the task exists, `TaskService` updates the allowed task fields.

15. `TaskService` updates the `Title` using the value provided in the request.

16. `TaskService` updates the `Completed` status using the value provided in the request.

17. `TaskService` does not modify the task `Id`.

18. `TaskService` does not modify the task `ProjectId`.

19. `TaskService` does not modify the task `CreatedAt`.

20. `TaskService` uses `IGenericRepository<Task>` to persist the updated task.

21. `GenericRepository<Task>` updates the task through `AppDbContext`.

22. Entity Framework Core persists the changes to the `Tasks` table in SQL Server.

23. `GenericRepository<Task>` returns the updated `Task` entity.

24. `TaskService` creates a successful `Result<Task>` containing the updated task.

25. `TaskService` returns the successful `Result<Task>` to `TasksController`.

26. `TasksController` converts the successful result into the unified `ApiResponse<Task>`.

27. API returns `200 OK`.

**### Success Response**

```json
{
  "success": true,
  "data": {
    "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
    "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Implement payment processing",
    "completed": true,
    "createdAt": "2026-09-08T11:00:00Z"
  }
}
```

HTTP status:

`200 OK`

**### Task Not Found**

If the specified task does not exist:

```json
{
  "success": false,
  "message": "Task not found.",
  "errors": [
    "Task not found."
  ]
}
```

HTTP status:

`404 Not Found`

**### Validation Failure**

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

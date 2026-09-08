**### Delete Task — Workflow**

****Endpoint****

`DELETE /api/v1/tasks/{id}`

****Input****

The client provides the task ID as a route parameter:

```text
DELETE /api/v1/tasks/7c9e6679-7425-40de-944b-e07fc1f90ae7
```

**### Workflow**

1. Client sends `DELETE /api/v1/tasks/{id}` with the task ID.

2. `TasksController` receives the HTTP request and binds the route parameter to a `Guid`.

3. `TasksController` calls `ITaskService.DeleteAsync(id)`.

4. `TaskService` attempts to retrieve the specified task.

5. `TaskService` uses `IGenericRepository<Task>` to retrieve the task by its ID.

6. `GenericRepository<Task>` queries the `Tasks` table through `AppDbContext`.

7. `GenericRepository<Task>` returns the matching `Task` entity if it exists, otherwise returns `null`.

8. If the task does not exist, `TaskService` detects that the repository returned `null`.

9. If the task was not found:

* `TaskService` creates a failure `Result` using the Result Pattern.

* The failure represents that the requested task does not exist.

* `TaskService` returns the failure `Result` to `TasksController`.

10. `TasksController` converts the failed `Result` into the unified `ApiResponse`.

11. API returns `404 Not Found`.

12. If the task exists, `TaskService` uses `IGenericRepository<Task>` to delete the task.

13. `GenericRepository<Task>` removes the task from `AppDbContext`.

14. Entity Framework Core persists the deletion to the `Tasks` table in SQL Server.

15. `TaskService` creates a successful `Result` indicating that the task was deleted successfully.

16. `TaskService` returns the successful `Result` to `TasksController`.

17. `TasksController` converts the successful result into the unified `ApiResponse`.

18. API returns `204 No Content`.

**### Success Response**

Since the task was successfully deleted, the API does not return a response body.

HTTP status:

`204 No Content`

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

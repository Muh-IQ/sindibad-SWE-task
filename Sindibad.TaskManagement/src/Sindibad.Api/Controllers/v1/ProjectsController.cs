using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sindibad.Api.Contracts;
using Sindibad.Api.Extensions;
using Sindibad.Api.Requests;
using Sindibad.Application.Common.Results;
using Sindibad.Application.DTOs;
using Sindibad.Application.IServices;
using Sindibad.Application.Services;
using Sindibad.Domain.Entities;
using Task = Sindibad.Domain.Entities.Task;

namespace Sindibad.Api.Controllers.v1
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController(IProjectService projectService, ITaskService taskService) : ControllerBase
    {
        #region Create Project
        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        /// <code>
        /// POST /api/v1/projects
        /// Content-Type: application/json
        ///
        /// {
        ///     "name": "Payment Platform"
        /// }
        /// </code>
        /// 
        /// Example successful response:
        /// 
        /// <code>
        /// HTTP 201 Created
        ///
        /// {
        ///     "success": true,
        ///     "data": {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "name": "Payment Platform",
        ///         "createdAt": "2026-09-08T10:30:00Z"
        ///     }
        /// }
        /// </code>
        /// 
        /// Example validation error:
        /// 
        /// <code>
        /// HTTP 400 Bad Request
        ///
        /// {
        ///     "success": false,
        ///     "message": "Validation Error",
        ///     "errors": [
        ///         "Project name is required."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="request">The project creation request.</param>
        /// <returns>The created project.</returns>
        /// <response code="201">The project was created successfully.</response>
        /// <response code="400">The request contains invalid data.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Project>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProjectRequest request)
        {
            Result<ProjectDTO> result = await projectService.CreateAsync(request.Name);
            return result.ToHttpResult(StatusCodes.Status201Created);
        }
        #endregion

        #region Get All Projects

        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        /// <remarks>
        /// This endpoint returns all projects currently stored in the system.
        ///
        /// Example request:
        ///
        /// <code>
        /// GET /api/v1/projects
        /// </code>
        ///
        /// Example successful response:
        ///
        /// <code>
        /// HTTP 200 OK
        ///
        /// {
        ///     "success": true,
        ///     "data": [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "name": "Payment Platform",
        ///             "createdAt": "2026-09-08T10:30:00Z"
        ///         },
        ///         {
        ///             "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
        ///             "name": "School Management System",
        ///             "createdAt": "2026-09-08T11:00:00Z"
        ///         }
        ///     ]
        /// }
        /// </code>
        ///
        /// Example response when no projects are found:
        ///
        /// <code>
        /// HTTP 404 Not Found
        ///
        /// {
        ///     "success": false,
        ///     "message": "No projects found.",
        ///     "errors": [
        ///         "No projects found."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <returns>A list of projects.</returns>
        /// <response code="200">Projects were retrieved successfully.</response>
        /// <response code="404">No projects were found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ProjectDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await projectService.GetAllAsync();

            return result.ToHttpResult();
        }
        #endregion


        #region Get Project By Id

        /// <summary>
        /// Retrieves a project by its ID together with all of its tasks.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///
        /// <code>
        /// GET /api/v1/projects/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </code>
        ///
        /// Example successful response:
        ///
        /// <code>
        /// HTTP 200 OK
        ///
        /// {
        ///     "success": true,
        ///     "data": {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "name": "Payment Platform",
        ///         "createdAt": "2026-09-08T10:30:00Z",
        ///         "tasks": [
        ///             {
        ///                 "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
        ///                 "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "title": "Implement payment processing",
        ///                 "completed": false,
        ///                 "createdAt": "2026-09-08T11:00:00Z"
        ///             }
        ///         ]
        ///     }
        /// }
        /// </code>
        ///
        /// Example response when the project does not exist:
        ///
        /// <code>
        /// HTTP 404 Not Found
        ///
        /// {
        ///     "success": false,
        ///     "message": "Project not found.",
        ///     "errors": [
        ///         "Project not found."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="id">The unique identifier of the project.</param>
        /// <returns>The requested project with all of its tasks.</returns>
        /// <response code="200">The project was retrieved successfully.</response>
        /// <response code="404">The specified project was not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Project>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await projectService.GetByIdAsync(id);

            return result.ToHttpResult();
        }

        #endregion

        #region Create Task

        /// <summary>
        /// Creates a new task for the specified project.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///
        /// <code>
        /// POST /api/v1/projects/3fa85f64-5717-4562-b3fc-2c963f66afa6/tasks
        /// Content-Type: application/json
        ///
        /// {
        ///     "title": "Implement payment processing"
        /// }
        /// </code>
        ///
        /// Example successful response:
        ///
        /// <code>
        /// HTTP 201 Created
        ///
        /// {
        ///     "success": true,
        ///     "data": {
        ///         "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
        ///         "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "title": "Implement payment processing",
        ///         "completed": false,
        ///         "createdAt": "2026-09-08T11:00:00Z"
        ///     }
        /// }
        /// </code>
        ///
        /// Example response when the project does not exist:
        ///
        /// <code>
        /// HTTP 404 Not Found
        ///
        /// {
        ///     "success": false,
        ///     "message": "Project not found.",
        ///     "errors": [
        ///         "Project not found."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="projectId">The unique identifier of the project.</param>
        /// <param name="request">The task creation request.</param>
        /// <returns>The created task.</returns>
        /// <response code="201">The task was created successfully.</response>
        /// <response code="400">The request contains invalid data.</response>
        /// <response code="404">The specified project was not found.</response>
        [HttpPost("/{projectId:guid}/tasks")]
        [ProducesResponseType(typeof(ApiResponse<Task>),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(Guid projectId,CreateTaskRequest request)
        {
            var result = await taskService.CreateAsync(projectId,request.Title);

            return result.ToHttpResult(StatusCodes.Status201Created);
        }

        #endregion
    }
}

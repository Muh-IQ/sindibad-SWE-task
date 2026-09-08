using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sindibad.Api.Contracts;
using Sindibad.Api.Extensions;
using Sindibad.Api.Requests;
using Sindibad.Application.Common.Results;
using Sindibad.Application.DTOs;
using Sindibad.Application.IServices;
using Sindibad.Domain.Entities;

namespace Sindibad.Api.Controllers.v1
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
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
        ///     "message": "Operation Succeeded",
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
    }
}

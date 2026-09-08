using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sindibad.Api.Contracts;
using Sindibad.Api.Extensions;
using Sindibad.Api.Requests;
using Sindibad.Application.IServices;
using Sindibad.Application.Services;
using Task = Sindibad.Domain.Entities.Task;
namespace Sindibad.Api.Controllers.v1
{
    [Route("api/v1/tasks")]
    [ApiController]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        #region Update Task

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///
        /// <code>
        /// PUT /api/v1/tasks/7c9e6679-7425-40de-944b-e07fc1f90ae7
        /// Content-Type: application/json
        ///
        /// {
        ///     "title": "Implement payment processing",
        ///     "completed": true
        /// }
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
        ///         "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
        ///         "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "title": "Implement payment processing",
        ///         "completed": true,
        ///         "createdAt": "2026-09-08T11:00:00Z"
        ///     }
        /// }
        /// </code>
        ///
        /// Example response when the task does not exist:
        ///
        /// <code>
        /// HTTP 404 Not Found
        ///
        /// {
        ///     "success": false,
        ///     "message": "Task not found.",
        ///     "errors": [
        ///         "Task not found."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="id">The unique identifier of the task.</param>
        /// <param name="request">The updated task data.</param>
        /// <returns>The updated task.</returns>
        /// <response code="200">The task was updated successfully.</response>
        /// <response code="400">The request contains invalid data.</response>
        /// <response code="404">The specified task was not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Task>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id,UpdateTaskRequest request)
        {
            var result = await taskService.UpdateAsync(id,request.Title,request.Completed);

            return result.ToHttpResult();
        }

        #endregion

        #region Delete Task

        /// <summary>
        /// Deletes an existing task.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///
        /// <code>
        /// DELETE /api/v1/tasks/7c9e6679-7425-40de-944b-e07fc1f90ae7
        /// </code>
        ///
        /// Example successful response:
        ///
        /// <code>
        /// HTTP 204 No Content
        /// </code>
        ///
        /// Example response when the task does not exist:
        ///
        /// <code>
        /// HTTP 404 Not Found
        ///
        /// {
        ///     "success": false,
        ///     "message": "Task not found.",
        ///     "errors": [
        ///         "Task not found."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="id">The unique identifier of the task.</param>
        /// <response code="204">The task was deleted successfully.</response>
        /// <response code="404">The specified task was not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await taskService.DeleteAsync(id);

            return result.ToHttpResult(StatusCodes.Status204NoContent);
        }

        #endregion
    }
}

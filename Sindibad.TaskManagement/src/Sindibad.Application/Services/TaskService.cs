using Sindibad.Application.Common.Results;
using Sindibad.Application.IRepositories;
using Sindibad.Application.IServices;
using Sindibad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Sindibad.Domain.Entities.Task;

namespace Sindibad.Application.Services;

public class TaskService(IGenericRepository<Project> projectRepository,IGenericRepository<Task> taskRepository) : ITaskService
{
   

    public async Task<Result<Task>> CreateAsync(Guid projectId, string title)
    {
        var projectExists = await projectRepository.ExistsAsync(p => p.Id == projectId);

        if (!projectExists)
        {
            return Result<Task>.Failure(ErrorType.NotFound, "Project not found.");
        }

        var task = new Task
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            Title = title,
            Completed = false,
            CreatedAt = DateTime.UtcNow
        };

        var createdTask =
            await taskRepository.AddAsync(task);

        return Result<Task>.Success(createdTask);
    }

   
}
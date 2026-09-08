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

public class TaskService(IGenericRepository<Project> projectRepository,IGenericRepository<Task> taskRepository, IGenericRepository<Task> GtaskRepository) : ITaskService
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

    public async Task<Result<Task>> UpdateAsync(Guid id, string title, bool completed)
    {
        var task = await taskRepository.GetByIdAsync(t => t.Id == id);

        if (task is null)
        {
            return Result<Task>.Failure(ErrorType.NotFound,"Task not found.");
        }

        task.Title = title;
        task.Completed = completed;

        await taskRepository.UpdateAsync(task);

        return Result<Task>.Success(task);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var task = await taskRepository.GetByIdAsync(t => t.Id == id);

        if (task is null)
        {
            return Result.Failure(ErrorType.NotFound, "Task not found.");
        }

        await taskRepository.DeleteAsync(task);

        return Result.Success();
    }
}
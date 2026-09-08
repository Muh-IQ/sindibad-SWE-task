using Sindibad.Application.Common.Mapper;
using Sindibad.Application.Common.Results;
using Sindibad.Application.DTOs;
using Sindibad.Application.IRepositories;
using Sindibad.Application.IServices;
using Sindibad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.Services;

public class ProjectService(IGenericRepository<Project> repository, IProjectRepository projectRepository) : IProjectService
{

    public async Task<Result<ProjectDTO>> CreateAsync(string projectName)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = projectName,
            CreatedAt = DateTime.UtcNow
        };

        var createdProject = await repository.AddAsync(project);
        var dto = ProjectMapper.ToDTO(createdProject);
        return Result<ProjectDTO>.Success(dto);
    }

    public async Task<Result<List<ProjectDTO>>> GetAllAsync()
    {
        var projects = await projectRepository.GetAllAsync();

        if (projects.Count == 0)
        {
            return Result<List<ProjectDTO>>.Failure(ErrorType.NotFound,"No projects found.");
        }

        return Result<List<ProjectDTO>>.Success(projects);
    }

}

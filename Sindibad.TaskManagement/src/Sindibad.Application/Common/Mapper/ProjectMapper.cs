using Sindibad.Application.DTOs;
using Sindibad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.Common.Mapper;

internal static class ProjectMapper
{
    public static ProjectDTO ToDTO(Project project)
    {
        return new ProjectDTO
        {
            Id = project.Id,
            Name = project.Name,
            CreatedAt = project.CreatedAt
        };
    }
}

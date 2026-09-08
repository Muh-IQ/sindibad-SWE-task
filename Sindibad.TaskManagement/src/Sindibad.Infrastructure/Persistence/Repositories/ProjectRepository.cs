using Microsoft.EntityFrameworkCore;
using Sindibad.Application.DTOs;
using Sindibad.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository(AppDbContext context) : IProjectRepository
    {
        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            return await context.Projects
                .Select(project => new ProjectDTO
                {
                    Id = project.Id,
                    Name = project.Name,
                    CreatedAt = project.CreatedAt
                })
                .ToListAsync();
        }
    }
}

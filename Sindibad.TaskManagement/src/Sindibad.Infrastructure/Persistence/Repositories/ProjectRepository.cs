using Microsoft.EntityFrameworkCore;
using Sindibad.Application.DTOs;
using Sindibad.Application.IRepositories;
using Sindibad.Domain.Entities;
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

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

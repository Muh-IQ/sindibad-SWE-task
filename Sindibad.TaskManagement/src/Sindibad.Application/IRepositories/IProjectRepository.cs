using Sindibad.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.IRepositories
{
    public interface IProjectRepository
    {
        Task<List<ProjectDTO>> GetAllAsync();
    }
}

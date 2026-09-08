using Sindibad.Application.Common.Results;
using Sindibad.Application.DTOs;
using Sindibad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.IServices;

public interface IProjectService
{
    Task<Result<ProjectDTO>> CreateAsync(string projectName);
    Task<Result<List<ProjectDTO>>> GetAllAsync();
}
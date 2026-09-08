using Sindibad.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sindibad.Application.IServices;

public interface ITaskService
{
    Task<Result<Sindibad.Domain.Entities.Task>> CreateAsync(Guid projectId, string title);
}
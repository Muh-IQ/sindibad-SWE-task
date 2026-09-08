using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.IRepositories;

public interface IGenericRepository<T>
{
    Task<T> AddAsync(T entity);
}
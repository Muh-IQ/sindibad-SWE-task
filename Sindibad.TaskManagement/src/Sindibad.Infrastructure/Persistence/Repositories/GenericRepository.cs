using Microsoft.EntityFrameworkCore;
using Sindibad.Application.IRepositories;
using Sindibad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class 
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context
            .Set<T>()
            .AsNoTracking()
            .AnyAsync(predicate);
    }

    public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context
            .Set<T>()
            .FirstOrDefaultAsync(predicate);
    }
    public async System.Threading.Tasks.Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);

        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);

        await _context.SaveChangesAsync();
    }
}
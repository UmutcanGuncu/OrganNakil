using Microsoft.EntityFrameworkCore;
using OrganNakil.Application.Interfaces;
using OrganNakil.Persistence.Context;

namespace OrganNakil.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T: class
{
    private readonly OrganNakilDbContext _context;

    public GenericRepository(OrganNakilDbContext context)
    {
        _context = context;
    }

    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T t)
    {
         var value =await _context.Set<T>().AddAsync(t);
         await _context.SaveChangesAsync();
         if (value.State == EntityState.Unchanged)
         {
             return value.Entity;
         }

         return null;
    }

    public Task UpdateAsync(T t)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T t)
    {
        throw new NotImplementedException();
    }
}
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

    public Task CreateAsync(T t)
    {
        throw new NotImplementedException();
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
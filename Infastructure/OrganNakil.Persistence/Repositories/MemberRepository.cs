using OrganNakil.Application.Interfaces;
using OrganNakil.Domain.Entities;
using OrganNakil.Persistence.Context;

namespace OrganNakil.Persistence.Repositories;

public class MemberRepository:IGenericRepository<AppUser>, IMemberRepository
{
    private readonly OrganNakilDbContext _context;

    public MemberRepository(OrganNakilDbContext context)
    {
        _context = context;
    }

    public Task<List<AppUser>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser> GetByIdAsync(Guid id)
    {
        return await _context.Set<AppUser>().FindAsync(id);
    }

    public Task<AppUser> CreateAsync(AppUser t)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(AppUser t)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AppUser t)
    {
        throw new NotImplementedException();
    }
} 
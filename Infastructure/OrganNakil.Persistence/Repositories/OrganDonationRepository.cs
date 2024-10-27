using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganNakil.Application.Dtos.OrganDonationRequestDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Domain.Entities;
using OrganNakil.Persistence.Context;

namespace OrganNakil.Persistence.Repositories;

public class OrganDonationRepository : IOrganDonationRepository
{
    private readonly OrganNakilDbContext _context;
    private readonly UserManager<AppUser> _userManager;


    public OrganDonationRepository(OrganNakilDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<OrganDonationRequest> AddOrganDonationRequest(CreateOrganDonationRequestDto createOrganDonationRequestDto)
    {
        var organ = await _context.Set<Organ>().Where(x => x.Name == createOrganDonationRequestDto.OrganName)
            .FirstOrDefaultAsync();
        var user = await _userManager.FindByIdAsync(createOrganDonationRequestDto.AppUser_Id.ToString());
        var value = await _context.Set<OrganDonationRequest>().AddAsync(new OrganDonationRequest()
        {
            AppUserId = user.Id,
            OrganId = organ.Id
        });
        await _context.SaveChangesAsync();
        if (value.State == EntityState.Unchanged)
        {
            return value.Entity;
        }

        return null;
    }

    public async Task<List<GetOrganDonationRequestDto>> GetActiveOrganDonationRequest()
    {
        var value = await _context.OrganDonationRequests.Include(x => x.AppUser).Include(x => x.Organ)
            .Where(x => x.IsDeleted == false).ToListAsync();

        var getOrganDonationRequestDtos = value.Select(x => new GetOrganDonationRequestDto()
        {
            AppUserId = x.AppUserId,
            BloodGroup = x.AppUser.BloodGroup,
            CreatedDate = x.CreatedDate,
            Id = x.Id,
            Name = x.AppUser.Name,
            OrganId = x.OrganId,
            OrganName = x.Organ.Name,
            PhoneNumber = x.AppUser.PhoneNumber,
            Surname = x.AppUser.Surname
        }).ToList();
        

        return getOrganDonationRequestDtos;
    }
}
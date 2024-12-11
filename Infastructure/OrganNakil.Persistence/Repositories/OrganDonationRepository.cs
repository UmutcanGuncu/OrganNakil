using System.Globalization;
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
        var deneme = await _context.Set<OrganDonationRequest>()
            .Where(x => x.OrganId == organ.Id && x.AppUserId == user.Id).ToListAsync();
        if (deneme.Count == 0)
        {
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
            City = x.AppUser.City,
            Name = x.AppUser.Name,
            OrganId = x.OrganId,
            OrganName = x.Organ.Name,
            PhoneNumber = x.AppUser.PhoneNumber,
            Surname = x.AppUser.Surname
        }).OrderByDescending(x=>x.CreatedDate).ToList();
        

        return getOrganDonationRequestDtos;
    }

    public async Task<List<GetOrganDonationRequestDto>> GetFilteredOrganDonationRequest(string city = null, string bloodType = null, string organ = null)
    {
        var query =  _context.OrganDonationRequests.Include(x => x.AppUser).Include(x => x.Organ).AsQueryable();
        
        if (!string.IsNullOrEmpty(city))
        {
            city = city.ToLower(new CultureInfo("tr-TR"));
            query = query.Where(x => EF.Functions.ILike(x.AppUser.City,city)&& x.IsDeleted == false);
        }
        
        if (!string.IsNullOrEmpty(bloodType))
        {
            query = query.Where(x => x.AppUser.BloodGroup.ToLower() == bloodType.ToLower() && x.IsDeleted == false);
        }

        if (!string.IsNullOrEmpty(organ))
        {
            query = query.Where(x => x.Organ.Name.ToLower() == organ.ToLower() && x.IsDeleted == false);
        }
        var values = await query.OrderByDescending(x => x.CreatedDate).ToListAsync();
        var getFilteredOrganDonationRequestDto = values.Select(x => new GetOrganDonationRequestDto()
        {
            AppUserId = x.AppUserId,
            BloodGroup = x.AppUser.BloodGroup,
            CreatedDate = x.CreatedDate,
            Id = x.Id,
            City = x.AppUser.City,
            Name = x.AppUser.Name,
            OrganId = x.OrganId,
            OrganName = x.Organ.Name,
            PhoneNumber = x.AppUser.PhoneNumber,
            Surname = x.AppUser.Surname
        }).ToList();
        return getFilteredOrganDonationRequestDto;
    }
}
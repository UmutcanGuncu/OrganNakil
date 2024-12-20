using OrganNakil.Application.Dtos.OrganDonationRequestDtos;
using OrganNakil.Domain.Entities;

namespace OrganNakil.Application.Interfaces;

public interface IOrganDonationRepository
{
    public Task<OrganDonationRequest> AddOrganDonationRequest(CreateOrganDonationRequestDto createOrganDonationRequestDto);
    public Task<List<GetOrganDonationRequestDto>> GetActiveOrganDonationRequest();
    public Task<List<GetOrganDonationRequestDto>> GetFilteredOrganDonationRequest(string city = null, string bloodType = null, string organ = null);
}
using MediatR;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Queries.OrganDonationRequestQueries;
using OrganNakil.Application.Mediatr.Results.OrganDonationRequestResults;

namespace OrganNakil.Application.Mediatr.Handlers.OrganDonationRequestHandler;

public class GetFilteredOrganDonationQueryHandler : IRequestHandler<GetFilteredOrganDonationRequestQuery, List<GetFilteredOrganDonationRequestQueryResult>>
{
    private readonly IOrganDonationRepository _organDonationRepository;

    public GetFilteredOrganDonationQueryHandler(IOrganDonationRepository organDonationRepository)
    {
        _organDonationRepository = organDonationRepository;
    }


    public async Task<List<GetFilteredOrganDonationRequestQueryResult>> Handle(GetFilteredOrganDonationRequestQuery request, CancellationToken cancellationToken)
    {
        var values =
            await _organDonationRepository.GetFilteredOrganDonationRequest(request.City, request.BloodType,
                request.Organ);
        return values.Select(x =>
            new GetFilteredOrganDonationRequestQueryResult()
            {
                AppUserId = x.AppUserId,
                BloodGroup = x.BloodGroup,
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name = x.Name,
                OrganId = x.OrganId,
                OrganName = x.OrganName,
                PhoneNumber = x.PhoneNumber,
                Surname = x.Surname
                
            }).ToList();
    }
}
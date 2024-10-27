using MediatR;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Queries.OrganDonationRequestQueries;
using OrganNakil.Application.Mediatr.Results.OrganDonationRequestResults;

namespace OrganNakil.Application.Mediatr.Handlers.OrganDonationRequestHandler;

public class GetActiveOrganDonationQueryHandler : IRequestHandler<GetActiveOrganDonationRequestQuery,List<GetActiveOrganDonationRequestQueryResult>>
{
    private readonly IOrganDonationRepository _repository;

    public GetActiveOrganDonationQueryHandler(IOrganDonationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetActiveOrganDonationRequestQueryResult>> Handle(GetActiveOrganDonationRequestQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetActiveOrganDonationRequest();
        return values.Select(x => new GetActiveOrganDonationRequestQueryResult()
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
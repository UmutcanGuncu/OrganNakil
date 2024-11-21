using MediatR;
using OrganNakil.Application.Mediatr.Results.OrganDonationRequestResults;
using OrganNakil.Domain.Entities;

namespace OrganNakil.Application.Mediatr.Queries.OrganDonationRequestQueries;

public class GetFilteredOrganDonationRequestQuery : IRequest<List<GetFilteredOrganDonationRequestQueryResult>>
{
   public GetFilteredOrganDonationRequestQuery(string city, string bloodType, string organ)
   {
      City = city;
      BloodType = bloodType;
      Organ = organ;
   }
   public string City { get; set; }
   public string BloodType { get; set; }
   public string Organ { get; set; }
}
using MediatR;
using OrganNakil.Application.Dtos.OrganDonationRequestDtos;

namespace OrganNakil.Application.Mediatr.Commands.OrganDonationRequestCommand;

public class CreateOrganDonationRequestCommand : IRequest<OrganDonationRequestStatusDto>
{
    public Guid AppUserId { get; set; }
    public string OrganName { get; set; }
}
using MediatR;
using OrganNakil.Application.Dtos.OrganDonationRequestDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Commands.OrganDonationRequestCommand;

namespace OrganNakil.Application.Mediatr.Handlers.OrganDonationRequestHandler;

public class CreateOrganDonationRequestCommandHandler : IRequestHandler<CreateOrganDonationRequestCommand, OrganDonationRequestStatusDto>
{
    private readonly IOrganDonationRepository _donationRepository;

    public CreateOrganDonationRequestCommandHandler(IOrganDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
    }

    public async Task<OrganDonationRequestStatusDto> Handle(CreateOrganDonationRequestCommand request, CancellationToken cancellationToken)
    {
        var value = await _donationRepository.AddOrganDonationRequest(new()
        {
            AppUser_Id = request.AppUserId,
            OrganName = request.OrganName
        });
        if (value != null)
        {
            return new()
            {
                Code = "Success",
                Description = "Organ Bağış Talebiniz Başarıyla Kaydedilmiştir"
            };
        }

        return new()
        {
            Code = "Fail",
            Description = "Organ Bağış Talebiniz Kaydedilememiştir"
        };
    }
}
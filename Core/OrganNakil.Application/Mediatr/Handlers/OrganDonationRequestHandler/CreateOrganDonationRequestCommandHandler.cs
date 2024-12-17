using MediatR;
using Microsoft.Extensions.Logging;
using OrganNakil.Application.Dtos.OrganDonationRequestDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Commands.OrganDonationRequestCommand;

namespace OrganNakil.Application.Mediatr.Handlers.OrganDonationRequestHandler;

public class CreateOrganDonationRequestCommandHandler : IRequestHandler<CreateOrganDonationRequestCommand, OrganDonationRequestStatusDto>
{
    private readonly IOrganDonationRepository _donationRepository;
    private readonly ILogger<CreateOrganDonationRequestCommandHandler> _logger;


    public CreateOrganDonationRequestCommandHandler(IOrganDonationRepository donationRepository, ILogger<CreateOrganDonationRequestCommandHandler> logger)
    {
        _donationRepository = donationRepository;
        _logger = logger;
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
            _logger.LogInformation($"Organ Bağış Talebi Kaydedildi {value.Id}");
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
using MediatR;
using OrganNakil.Application.Dtos.OrganDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Commands.OrganCommands;
using OrganNakil.Domain.Entities;

namespace OrganNakil.Application.Mediatr.Handlers.OrganHandlers;

public class CreateOrganCommandHandler : IRequestHandler<CreateOrganCommand, OrganStatusDto>
{
    private readonly IGenericRepository<Organ> _genericRepository;

    public CreateOrganCommandHandler(IGenericRepository<Organ> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<OrganStatusDto> Handle(CreateOrganCommand request, CancellationToken cancellationToken)
    {
        var value =  await _genericRepository.CreateAsync(new()
        {
            Name = request.Name
        });
        if (value != null)
        {
            return new OrganStatusDto()
            {
                Code = "Success",
                Description = "Organ Bilgisi Başarıyla Eklendi",
                OrganId = value.Id
            };
        }

        return new OrganStatusDto()
        {
            Code = "Failed",
            Description = "Organ Bilgisi Eklenemedi"
        };

    }
}
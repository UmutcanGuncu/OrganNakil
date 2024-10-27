using MediatR;
using OrganNakil.Application.Dtos.OrganDtos;

namespace OrganNakil.Application.Mediatr.Commands.OrganCommands;

public class CreateOrganCommand : IRequest<OrganStatusDto>
{
    public string Name { get; set; }
}
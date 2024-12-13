using MediatR;
using OrganNakil.Application.Dtos.TokenDtos;
using OrganNakil.Application.Mediatr.Queries.RefreshTokenQueries;

namespace OrganNakil.Application.Mediatr.Commands.RefreshTokenCommands;

public class RefreshTokenLoginCommand : IRequest<RefreshTokenLoginQuery>
{
    public string RefreshToken { get; set; }
}
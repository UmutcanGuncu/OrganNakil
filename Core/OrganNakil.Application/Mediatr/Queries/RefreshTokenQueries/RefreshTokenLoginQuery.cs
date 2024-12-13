using MediatR;
using OrganNakil.Application.Dtos.TokenDtos;
using OrganNakil.Application.Mediatr.Results.RefreshTokenResults;

namespace OrganNakil.Application.Mediatr.Queries.RefreshTokenQueries;

public class RefreshTokenLoginQuery : IRequest<RefreshTokenLoginQueryResult>
{
    public string RefreshToken { get; set; }
    
}
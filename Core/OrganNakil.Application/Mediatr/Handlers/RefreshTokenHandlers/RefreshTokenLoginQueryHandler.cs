using MediatR;
using OrganNakil.Application.Dtos.TokenDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Queries.RefreshTokenQueries;
using OrganNakil.Application.Mediatr.Results.RefreshTokenResults;

namespace OrganNakil.Application.Mediatr.Handlers.RefreshTokenHandlers;

public class RefreshTokenLoginQueryHandler : IRequestHandler<RefreshTokenLoginQuery,RefreshTokenLoginQueryResult>
{
    private readonly IUserRepository _userRepository;

    public RefreshTokenLoginQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RefreshTokenLoginQueryResult> Handle(RefreshTokenLoginQuery request, CancellationToken cancellationToken)
    {
        Token token = await _userRepository.RefreshTokenLoginAsync(request.RefreshToken);
        return new()
        {
            Token = token,
        };
    }
}
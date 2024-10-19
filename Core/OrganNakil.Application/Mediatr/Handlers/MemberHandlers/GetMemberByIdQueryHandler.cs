using MediatR;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Queries.MemberQueries;
using OrganNakil.Application.Mediatr.Results.MemberResults;
using OrganNakil.Domain.Entities;

namespace OrganNakil.Application.Mediatr.Handlers.MemberHandlers;

public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, GetMemberByIdQueryResult>
{
    private readonly IGenericRepository<AppUser> _repository;

    public GetMemberByIdQueryHandler(IGenericRepository<AppUser> repository)
    {
        _repository = repository;
    }

    public async Task<GetMemberByIdQueryResult> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetByIdAsync(request.Id);
        return new()
        {
            Id = value.Id,
            Email = value.Email,
            Name = value.Name,
            Surname = value.Surname,
            PhoneNumber = value.PhoneNumber,
            Tc = value.UserName
        };
    }
}
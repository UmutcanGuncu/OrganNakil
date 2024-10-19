using MediatR;
using OrganNakil.Application.Mediatr.Results.MemberResults;

namespace OrganNakil.Application.Mediatr.Queries.MemberQueries;

public class GetMemberByIdQuery : IRequest<GetMemberByIdQueryResult>
{
    public GetMemberByIdQuery(Guid ıd)
    {
        Id = ıd;
    }

    public Guid Id { get; set; }
    
}
namespace OrganNakil.Application.Mediatr.Results.MemberResults;

public class GetMemberByIdQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Tc { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string BloodGroup { get; set; }
    
}
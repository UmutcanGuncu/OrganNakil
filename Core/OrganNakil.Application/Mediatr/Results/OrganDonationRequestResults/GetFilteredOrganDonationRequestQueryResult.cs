namespace OrganNakil.Application.Mediatr.Results.OrganDonationRequestResults;

public class GetFilteredOrganDonationRequestQueryResult
{
    public Guid Id { get; set; }
    public Guid OrganId { get; set; }
    public string OrganName { get; set; }
    public Guid AppUserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string BloodGroup { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public DateTime CreatedDate { get; set; }
}
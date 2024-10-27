using OrganNakil.Domain.Common;

namespace OrganNakil.Domain.Entities;

public class OrganDonationRequest : BaseEntity
{
    public Organ Organ { get; set; }
    public Guid OrganId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid AppUserId { get; set; }
    
}
using SharedKarnel;

namespace Domain.Entities;

public class Employee : EntityBase<int>, IAgencyFilter
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int TravelAgencyId { get; set; }
    public int CompanyId { get; set; }
    public int SystemUserId { get; set; }
    public Designation Designation { get; set; }

    public SystemUser SystemUser { get; set; }
    public TravelAgency TravelAgency { get; set; }
    public Company Company { get; set; }
}

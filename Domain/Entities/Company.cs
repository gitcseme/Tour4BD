using System.Collections.Generic;

namespace Domain.Entities;

public class Company : EntityBase<int>, IAgencyFilter
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string LisenceLink { get; set; }
    public bool IsActive { get; set; }
    public int TravelAgencyId { get; set; }

    public TravelAgency TravelAgency { get; set; }
    public ICollection<Package> Packages { get; set; } = [];
    public ICollection<Rating> Ratings { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
}

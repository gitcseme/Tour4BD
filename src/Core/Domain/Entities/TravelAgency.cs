using System.Collections.Generic;

namespace Domain.Entities;

public class TravelAgency : EntityBase<int>
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Address { get; set; }

    public ICollection<Company> Companies { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
}
